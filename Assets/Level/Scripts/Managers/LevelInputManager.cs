using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityStandardAssets.CrossPlatformInput;

public class LevelInputManager: IInitializable
{
    private readonly LevelConfig _levelConfig;
    private readonly LevelUIController _uiController;

    public LevelInputManager(LevelConfig levelConfig, LevelUIController uiController)
    {
        _levelConfig = levelConfig;
        _uiController = uiController;
    }

    public void Initialize()
    {
        if (_levelConfig.desktopControls)
        {
            CrossPlatformInputManager.SwitchActiveInputMethod(CrossPlatformInputManager.ActiveInputMethod.Hardware);
            _uiController.HideControls();
        }
    }
}
