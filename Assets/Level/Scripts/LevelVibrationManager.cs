using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelVibrationManager
{
    private SignalBus _signalBus;
    private AsyncProcessor _asyncProcessor;

    public LevelVibrationManager(SignalBus signalBus, AsyncProcessor asyncProcessor)
    {
        _signalBus = signalBus;
        _asyncProcessor = asyncProcessor;
        signalBus.Subscribe<PlayerChangedColorSignal>(s => this.OnPlayerChangedColor(s));
        signalBus.Subscribe<LevelEndSignal>(s => this.OnLevelEnd(s));
    }

    public void OnPlayerChangedColor(PlayerChangedColorSignal signal)
    {
        if (signal.oldColor != GameColor.NONE)
        {
            Vibrator.Vibrate(50);
        }
    }

    public void OnLevelEnd(LevelEndSignal signal)
    {
        _asyncProcessor.StartCoroutine(LevelEnd());
    }

    private IEnumerator LevelEnd()
    {
        yield return new WaitForSeconds(2.0f);
        Vibrator.Vibrate();
    }
}
