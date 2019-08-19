using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    public GameConfig configuration;

    // todo resolve Monobehaviours with  Container.Bind<Foo>().FromComponentInHierarchy().AsSingle();
    // todo or use ZenjectBinding 
    public override void InstallBindings()
    {
        Container.BindInstance(configuration);
        Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        Container.BindSignal<LevelEndSignal>().ToMethod<GameManager>(g => g.OnLevelEnd).FromResolve();
    }

    public void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<LevelEndSignal>();
    }

}

