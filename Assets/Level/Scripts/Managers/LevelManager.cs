﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : IInitializable
{
    private readonly ColorsManager _colorsManager;
    private readonly SignalBus _signalBus;
    private readonly AsyncProcessor _asyncProcessor;

    public LevelManager(ColorsManager colorsManager, SignalBus signalBus, AsyncProcessor asyncProcessor)
    {
        _colorsManager = colorsManager;
        _signalBus = signalBus;
        _asyncProcessor = asyncProcessor;
    }

    public void Initialize()
    {
        _signalBus.Fire(new LevelStartSignal());
    }

    public IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(1.0f);
       
    }

    public void OnPlayerTouched(PlayerTouchedSignal signal)
    {
        if (signal.objectTouchType == ObjectTouchType.LevelEnd)
        {
            _asyncProcessor.StartCoroutine(OnLevelEnd());
        }
    }

    public void OnPlayerChangeColor(PlayerChangedColorSignal signal)
    {
        Debug.Log(string.Format("OnPlayerChangeColor oldColor -> {0} newColor -> {1}", signal.oldColor, signal.newColor));
        _colorsManager.SetWorldColor(signal.newColor);
    }

    public IEnumerator OnLevelEnd()
    {
        _signalBus.Fire(new LevelEndSignal());
        yield return new WaitForSeconds(6.0f);
        // quit application, for now
        Application.Quit();
    }
}
