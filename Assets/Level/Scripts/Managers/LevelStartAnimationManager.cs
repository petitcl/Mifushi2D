using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelStartAnimationManager : IInitializable
{
    private readonly SignalBus _signalBus;
    private readonly AsyncProcessor _asyncProcessor;
    private readonly LevelConfig _levelConfig;
    private readonly PlayerController _playerController;
    private readonly InputPlayerController _inputPlayerController;
    private readonly LevelUIController _uiController;

    public LevelStartAnimationManager(
        SignalBus signalBus, AsyncProcessor asyncProcessor,
        LevelConfig levelConfig,
        LevelUIController uiController, PlayerController playerController, InputPlayerController inputPlayerController
    )
    {
        _signalBus = signalBus;
        _asyncProcessor = asyncProcessor;
        _levelConfig = levelConfig;
        _playerController = playerController;
        _inputPlayerController = inputPlayerController;
        _uiController = uiController;

        _signalBus.Subscribe<LevelStartSignal>(s => this.OnLevelStart(s));
    }

    public void Initialize()
    {
        if (!_levelConfig.startAnimationEnabled)
        {
            _inputPlayerController.CanMove = true;
            _uiController.HideOverlay();
            return;
        }
        _inputPlayerController.CanMove = false;
        _uiController.ShowOverlay();
    }

    public void OnLevelStart(LevelStartSignal signal)
    {
        if (!_levelConfig.startAnimationEnabled)
        {
            return;
        }
        Debug.Log("OnLevelStart");
        _asyncProcessor.StartCoroutine(PlayPlayerAnimation());
        _asyncProcessor.StartCoroutine(PlayUIAnimation());
    }

    public IEnumerator PlayPlayerAnimation()
    {
        // wait for end of frame so all game objects have time to initialize
        yield return new WaitForEndOfFrame();
        _playerController.Wave();
        yield return new WaitForSeconds(2.0f);
        _inputPlayerController.CanMove = true;
    }

    public IEnumerator PlayUIAnimation()
    {
        yield return new WaitForEndOfFrame();
        _uiController.OverlayFadeIn();
    }
}
