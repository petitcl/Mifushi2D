using Anima2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// todo: handle effector
// todo: handle multiple colliders
[AddComponentMenu("Scripts/Mifushi 2D/ColoredGameObject")]
[RequireComponent(typeof(Collider2D))]
public class ColoredGameObject : MonoBehaviour
{
    // Initial color of the game object
    [SerializeField]
    public GameColor initialColor = GameColor.WHITE;

    // Is the color of this game object managed by the world ?
    [SerializeField]
    public bool managed = true;

    // Current color of the game object
    public GameColor Color
    {
        get { return _color; }
        set { SetColor(value);  }
    }

    // Current layer mask of the game object
    public int GameColorLayerMask
    {
        get { return _colorsManager.GetCombinedLayerMask(Color); }
    }

    private ColorsManager _colorsManager;

    private SpriteRenderer[] _spriteRenderers;
    private SpriteMeshInstance[] _spriteMeshInstances;
    private Collider2D _collider2D;
    private ContactFilter2D _contactFilter2D;

    private GameColor _color = GameColor.NONE;
    private GameColor _previousColor;
    private Collider2D[] _collider2DsBuffer = null;
    private ColorAnimationManager colorAnimationManager = new ColorAnimationManager();
    private ColorAnimationManager transparencyAnimationManager = new ColorAnimationManager();
    private bool _isOpaque = false;

    [Inject]
    public void Init(ColorsManager colorsManager)
    {
        //Debug.Log("ColoredGameObject.Init " + gameObject.name);
        _colorsManager = colorsManager;
    }

    /// <summary>
    /// Change the color of this game object
    /// </summary>
    public bool SetColor(GameColor newColor, bool animation = true)
    {
        if (!CanChangeColor(newColor)) return false;
        _previousColor = _color;
        _color = newColor;
        gameObject.layer = _colorsManager.GetLayer(this._color);
        if (animation)
        {
            colorAnimationManager.StartAnimation();
        }
        else
        {
            DoUpdateColor(_colorsManager.GetColor(Color));
        }
        return true;
    }

    /// <summary>
    /// Cycle the color of this game object.
    /// Goes from Red -> Green -> Blue -> Red ...
    /// </summary>
    public bool CycleColor()
    {
        switch (Color)
        {
            case GameColor.RED:
                return SetColor(GameColor.GREEN);
            case GameColor.GREEN:
                return SetColor(GameColor.BLUE);
            case GameColor.BLUE:
                return SetColor(GameColor.RED);
            default:
                return false;
        }
    }


    public void OnWorldColorChange(GameColor oldWorldColor, GameColor newWorldColor)
    {
        //Debug.Log(String.Format("OnWorldColorChange gameObject={0}, oldWorldColor={1}, newWorldColor={2}, Color={3}", gameObject.name, oldWorldColor, newWorldColor, Color));

        _isOpaque = Color == newWorldColor;
        // we don't play animations during the first color change
        if (oldWorldColor == GameColor.NONE)
        {
            DoUpdateColorTransparency(_isOpaque ? _colorsManager.OpaqueAlpha : _colorsManager.TransparentAlpha);
            return;
        }
        // if this colored object has not the same color as the world, make it transparent
        if (_isOpaque && oldWorldColor != Color)
        {
            transparencyAnimationManager.StartAnimation();
        }
        // if this colored object has the same color as the world, make it opaque
        else if (!_isOpaque && oldWorldColor == Color)
        {
            transparencyAnimationManager.StartAnimation();
        }
    }

    // todo: use only one global tweening instead of one per game object
    private void DoUpdateColor(Color color)
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.color = color;
        }
        foreach (SpriteMeshInstance spriteMeshInstance in _spriteMeshInstances)
        {
            spriteMeshInstance.color = color;
        }
    }

    private void DoUpdateColorTransparency(float newTransparency)
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            Color newColor = spriteRenderer.color;
            newColor.a = newTransparency;
            spriteRenderer.color = newColor;
        }
        foreach (SpriteMeshInstance spriteMeshInstance in _spriteMeshInstances)
        {
            Color newColor = spriteMeshInstance.color;
            newColor.a = newTransparency;
            spriteMeshInstance.color = newColor;
        }
    }

    private bool CanChangeColor(GameColor newColor)
    {
        if (managed) return true;
        _collider2DsBuffer = _collider2DsBuffer?? new Collider2D[16];

        _contactFilter2D.SetLayerMask(_colorsManager.GetLayerMask(newColor));

        if (_collider2D == null)
        {
            Debug.Log("ColoredGameObject#canChangeColor " + gameObject.name);
        }
        int count = _collider2D.OverlapCollider(_contactFilter2D, _collider2DsBuffer);
        //Debug.Log("ColoredGameObject#canChangeColor " + gameObject.name + " -> Got  " + count + " Collisions");
        for (int i = 0; i < count; ++i)
        {
            Debug.Log("ColoredGameObject#canChangeColor " + gameObject.name + " -> Collision detected with " + _collider2DsBuffer[i].gameObject.name);
        }
        return count == 0;
    }

    private void Start()
    {
        Debug.Log("ColoredGameObject.Start " + gameObject.name);
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _spriteMeshInstances = GetComponentsInChildren<SpriteMeshInstance>();
        _collider2D = GetComponent<Collider2D>();
        _contactFilter2D = new ContactFilter2D();
        _contactFilter2D.useTriggers = false;
        _contactFilter2D.useLayerMask = true;

        _previousColor = _color;
        SetColor(initialColor, false);
        if (managed)
        {
            _colorsManager.RegisterColoredGameObject(this);
        }
    }

    private void Update()
    {
        colorAnimationManager.Update();
        if (colorAnimationManager.IsTransitioning)
        {
            Color previousColor = _colorsManager.GetColor(_previousColor);
            Color targetColor = _colorsManager.GetColor(_color);
            Color newColor = UnityEngine.Color.Lerp(previousColor, targetColor, colorAnimationManager.Value);
            //Debug.Log("Update Color" + colorAnimationManager.Value);
            DoUpdateColor(newColor);
        }

        transparencyAnimationManager.Update();
        if (transparencyAnimationManager.IsTransitioning)
        {
            float previousTransparency = _isOpaque ? _colorsManager.TransparentAlpha : _colorsManager.OpaqueAlpha;
            float targetTransparency = _isOpaque ? _colorsManager.OpaqueAlpha : _colorsManager.TransparentAlpha;
            
            float newTransparency = Mathf.Lerp(previousTransparency, targetTransparency, transparencyAnimationManager.Value);
            //Debug.Log("Update Transparency" + transparencyAnimationManager.Value);
            DoUpdateColorTransparency(newTransparency);
        }
    }
}
