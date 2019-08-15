using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField]
    public JoystickPackJoystick joystickPackJoystick;

    public override void InstallBindings()
    {
        InstallSignals();

        Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle();
        Container.BindSignal<PlayerChangedColorSignal>()
            .ToMethod<LevelManager>(l => l.OnPlayerChangeColor).FromResolve();
        Container.BindSignal<PlayerTouchedSignal>()
            .ToMethod<LevelManager>(l => l.OnPlayerTouched).FromResolve();

        Container.BindInterfacesAndSelfTo<ColorsManager>().AsSingle();

        PlayerJoystick joystick = new CompositeJoystick(
            new KeyboardPlayerJoystick(),
            joystickPackJoystick
        );
        Container.Bind<PlayerJoystick>().FromInstance(joystick);
    }

    public void InstallSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PlayerChangedColorSignal>();
        Container.DeclareSignal<PlayerTouchedSignal>();
    }

}
