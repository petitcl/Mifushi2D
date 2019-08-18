using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameInstaller : MonoInstaller
{
    // todo use Settings
    // todo resolve Monobehaviours with  Container.Bind<Foo>().FromComponentInHierarchy().AsSingle();
    // todo or use ZenjectBinding 
    public override void InstallBindings()
    {
        Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        Container.BindSignal<LevelEndSignal>().ToMethod<GameManager>(g => g.OnLevelEnd).FromResolve();
        LevelInstaller.Install(Container);
    }

    public void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<LevelEndSignal>();
    }

}

