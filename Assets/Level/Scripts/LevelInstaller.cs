using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller<LevelInstaller>
{

    [SerializeField]
    public LevelConfig configuration;

    public override void InstallBindings()
    {
        InstallSignals();

        Container.BindInstance(configuration);
        Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ColorsManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LevelInputManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LevelVibrationManager>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<LevelStartAnimationManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LevelEndAnimationManager>().AsSingle().NonLazy();

        //todo: change to subscribe in target services
        Container.BindSignal<PlayerChangedColorSignal>().ToMethod<LevelManager>(l => l.OnPlayerChangeColor).FromResolve();
        Container.BindSignal<PlayerTouchedSignal>().ToMethod<LevelManager>(l => l.OnPlayerTouched).FromResolve();
    }

    public void InstallSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PlayerChangedColorSignal>();
        Container.DeclareSignal<PlayerTouchedSignal>();
        Container.DeclareSignal<LevelStartSignal>();
        Container.DeclareSignal<LevelEndSignal>();
    }

}
