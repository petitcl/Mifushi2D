using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField]
    public bool debugMode = false;

    public override void InstallBindings()
    {
        InstallSignals();

        Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle();
        Container.BindSignal<PlayerChangedColorSignal>()
            .ToMethod<LevelManager>(l => l.OnPlayerChangeColor).FromResolve();
        Container.BindSignal<PlayerTouchedSignal>()
            .ToMethod<LevelManager>(l => l.OnPlayerTouched).FromResolve();

        Container.BindInterfacesAndSelfTo<ColorsManager>().AsSingle();
        if (debugMode)
        {
            InstallDebugBindings();
        }
    }

    public void InstallSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PlayerChangedColorSignal>();
        Container.DeclareSignal<PlayerTouchedSignal>();
    }

    public void InstallDebugBindings()
    {
        // todo
    }
}
