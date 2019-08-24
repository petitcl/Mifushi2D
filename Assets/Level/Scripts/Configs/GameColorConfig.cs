using UnityEngine;
using System;

[Serializable]
public class GameColorConfig
{
    public GameColor gameColor;

    // the next game color to use when cycling
    public GameColor nextGameColor;

    // name of the corresponding physic layer
    public string layerName;

    // name of the game color
    public string colorName;

    // actual render color of the game color
    public Color color;

    // is the color static ? A static color is always interactable
    public bool isStatic;
}