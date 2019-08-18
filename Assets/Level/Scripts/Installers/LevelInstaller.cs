using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Zenject;

public class LevelInstaller : Installer<LevelInstaller>
{

    public override void InstallBindings()
    {
        InstallSignals();

        Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle();
        Container.BindSignal<PlayerChangedColorSignal>().ToMethod<LevelManager>(l => l.OnPlayerChangeColor).FromResolve();
        Container.BindSignal<PlayerTouchedSignal>().ToMethod<LevelManager>(l => l.OnPlayerTouched).FromResolve();
        Container.BindInterfacesAndSelfTo<LevelEndAnimationManager>().AsSingle();

        Container.BindInterfacesAndSelfTo<ColorsManager>().AsSingle();
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
