using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : IInitializable
{
    private ColorsManager _colorsManager;

    public LevelManager(ColorsManager colorsManager)
    {
        _colorsManager = colorsManager;
    }

    public void Initialize()
    {
        
    }

    public void OnPlayerTouched(PlayerTouchedSignal signal)
    {
        //player.transform.position = respawnPoint.position;
    }

    public void OnPlayerChangeColor(PlayerChangedColorSignal signal)
    {
        _colorsManager.SetWorldColor(signal.newColor);
    }

}
