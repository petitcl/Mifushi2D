using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsManager
{
    /// <summary>
    /// Configuration of a game color
    /// </summary>
    public class GameColorConfiguration
    {
        // name of the corresponding physic layer
        public string layerName;
        // name of the game color
        public string colorName;
        // actual render color of the game color
        public Color color;
    }

    // events
    public delegate void OnWorldColorChangeDelegate(GameColor oldColor, GameColor newColor);
    public event OnWorldColorChangeDelegate OnWorldColorChangeEvent;

    /// <summary>
    /// The current selected color
    /// </summary>
    public GameColor WorldColor { get { return _worldColor;  } }
    public float OpaqueAlpha { get { return 1.0f; } }
    public float TransparentAlpha { get { return 0.3f; } }

    private GameColor _worldColor = GameColor.NONE;
    private Dictionary<GameColor, GameColorConfiguration> _configurations;

    public ColorsManager()
    {
        _configurations = new Dictionary<GameColor, GameColorConfiguration>()
        {
            [GameColor.WHITE] = new GameColorConfiguration { color = Color.white,   colorName = "White",    layerName = "White" },
            [GameColor.RED] =   new GameColorConfiguration { color = Color.red,     colorName = "Red",      layerName = "Red"   },
            [GameColor.GREEN] = new GameColorConfiguration { color = Color.green,   colorName = "Green",    layerName = "Green" },
            [GameColor.BLUE] =  new GameColorConfiguration { color = Color.blue,    colorName = "Blue",     layerName = "Blue"  },
        };
    }

    public void OnPlayerChangeColor(PlayerChangedColorSignal signal)
    {
        Debug.Log("Player changed color to " + signal.newColor);
        SetWorldColor(signal.newColor);
    }

    /// <summary>
    /// Change the color of the world.
    /// Objects that are not from this color will become transparent.
    /// Objects that are from this color will become opaque.
    /// </summary>
    /// <param name="newColor"></param>
    public void SetWorldColor(GameColor newColor)
    {
        Debug.Log("SetWorldColor " + newColor);
        GameColor oldColor = _worldColor;
        _worldColor = newColor;
        OnWorldColorChangeEvent?.Invoke(oldColor, newColor);
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
        return _configurations[color].color;
    }

    /// <summary>
    /// Get the layer name corresponding to the given color
    /// </summary>
    public string GetLayerName(GameColor color)
    {
        return _configurations[color].layerName;
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

    /// <summary>
    /// Get the name corresponding to the given color
    /// </summary>
    public string GetColorName(GameColor color)
    {
        return _configurations[color].colorName;
    }

}
