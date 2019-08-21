using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelEndAnimationManager : IInitializable
{
    private readonly SignalBus _signalBus;
    private readonly AsyncProcessor _asyncProcessor;
    private readonly PlayerController _playerController;
    private readonly InputPlayerController _inputPlayerController;
    private readonly LevelUIController _uiController;

    public LevelEndAnimationManager(
        SignalBus signalBus, AsyncProcessor asyncProcessor,
        LevelUIController uiController,
        PlayerController playerController, InputPlayerController inputPlayerController
    )
    {
        _signalBus = signalBus;
        _asyncProcessor = asyncProcessor;
        _playerController = playerController;
        _inputPlayerController = inputPlayerController;
        _uiController = uiController;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<LevelEndSignal>(s => this.OnLevelEnd(s));
    }

    public void OnLevelEnd(LevelEndSignal signal)
    {
        _asyncProcessor.StartCoroutine(PlayPlayerAnimation());
        _asyncProcessor.StartCoroutine(PlayUIAnimation());
    }

    public IEnumerator PlayPlayerAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        _inputPlayerController.CanMove = false;
        yield return new WaitForSeconds(0.15f);
        _playerController.Jump();
    }

    public IEnumerator PlayUIAnimation()
    {
        _uiController.FadeOut();
        yield return new WaitForSeconds(2.5f);
        _uiController.Overlay.FadeIn();
    }
}
