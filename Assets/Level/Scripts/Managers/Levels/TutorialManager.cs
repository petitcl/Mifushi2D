using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TutorialManager : MonoBehaviour
{
    private SignalBus _signalBus;
    private LevelConfig _levelConfig;
    private PlayerController _playerController;
    private InputPlayerController _inputPlayerController;
    private LevelUIController _uiController;

    private int _colorChangeCount = 0;
    private GameColor _currentColor;

    [Inject]
    public void Init(
        SignalBus signalBus, 
        LevelConfig levelConfig, 
        PlayerController playerController, InputPlayerController inputPlayerController, LevelUIController uiController)
    {
        _signalBus = signalBus;
        _levelConfig = levelConfig;
        _playerController = playerController;
        _inputPlayerController = inputPlayerController;
        _uiController = uiController;
    }

    public void OnLevelStart(LevelStartSignal signal)
    {
        //Debug.Log("Level0AnimationsManager.OnLevelStart");
        StartCoroutine(PlayUIAnimation());
    }

    public IEnumerator PlayUIAnimation()
    {
         _uiController.HideControls();
         _uiController.ColorChangeButton.Animate = false;

        yield return new WaitForSeconds(3.0f);
        _uiController.Joystick.Fader.FadeIn();
    }

    public void OnPlayerChangedColor(PlayerChangedColorSignal signal)
    {
        _currentColor = signal.newColor;
        //Debug.Log(String.Format("TutorialManager.OnPlayerChangedColor oldColor -> {0}, newColor -> {1}", signal.oldColor, signal.newColor));
        if (signal.oldColor == GameColor.NONE)
        {
            return;
        }
        _colorChangeCount++;
        _uiController.ColorChangeButton.Button.Disable();
        _uiController.ColorChangeButton.Fader.FadeOut();
    }

    public void OnPlayerInFrontOfWall()
    {
        _uiController.ColorChangeButton.Refresh(_currentColor, false);
        _uiController.ColorChangeButton.Button.Enable();
        _uiController.ColorChangeButton.Fader.FadeIn();
    }

    private void Awake()
    {
        _signalBus.Subscribe<LevelStartSignal>(s => this.OnLevelStart(s));
        _signalBus.Subscribe<PlayerChangedColorSignal>(s => this.OnPlayerChangedColor(s));
    }
}
