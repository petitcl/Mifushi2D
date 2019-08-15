using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsManager
{
    // Color names
    public Color White { get { return Color.white; } }
    public Color Red { get { return Color.red; } }
    public Color Green { get { return Color.green; } }
    public Color Blue { get { return Color.blue; } }

    // Layer names
    public static string WHITE = "White";
    public static string RED = "Red";
    public static string GREEN = "Green";
    public static string BLUE = "Blue";

    // events
    public delegate void OnWorldColorChangeDelegate(GameColor newColor);
    public event OnWorldColorChangeDelegate OnWorldColorChangeEvent;

    /// <summary>
    /// The current selected color
    /// </summary>
    public GameColor WorldColor { get { return _worldColor;  } }

    private GameColor _worldColor = GameColor.WHITE;

    public void OnPlayerChangeColor(PlayerChangedColorSignal signal)
    {
        Debug.Log("Player changed color to " + signal.newColor);
        SetWorldColor(signal.newColor);
    }

    public void SetWorldColor(GameColor newColor)
    {
        _worldColor = newColor;
        OnWorldColorChangeEvent?.Invoke(newColor);
    }

    /// <summary>
    /// Register a colored game object. The object will be notified when the world color changes
    /// </summary>
    /// <param name="obj"></param>
    public void RegisterColoredGameObject(ColoredGameObject obj)
    {
        OnWorldColorChangeEvent += obj.OnWorldColorChange;
    }

    /// <summary>
    /// Return the unity color corresponding to the given color
    /// </summary>
    public Color GetColor(GameColor color)
    {
        switch (color)
        {
            case GameColor.BLUE:
                return Blue;
            case GameColor.RED:
                return Red;
            case GameColor.GREEN:
                return Green;
            case GameColor.WHITE:
            default:
                return White;
        }
    }

    /// <summary>
    /// Get the layer name corresponding to the given color
    /// </summary>
    public string GetLayerName(GameColor color)
    {
        switch (color)
        {
            case GameColor.BLUE:
                return ColorsManager.BLUE;
            case GameColor.RED:
                return ColorsManager.RED;
            case GameColor.GREEN:
                return ColorsManager.GREEN;
            case GameColor.WHITE:
            default:
                return ColorsManager.WHITE;
        }
    }

    public int GetLayer(GameColor color = GameColor.WHITE)
    {
        return LayerMask.NameToLayer(GetLayerName(color));
    }

    public int GetLayerMask(GameColor color = GameColor.WHITE)
    {
        return 1 << GetLayer(color);
    }

    public int GetCombinedLayerMask(GameColor color = GameColor.WHITE)
    {
        if (color == GameColor.WHITE)
        {
            return GetLayerMask(GameColor.WHITE);
        }
        return GetLayerMask(GameColor.WHITE) | GetLayerMask(color);
    }

}
