using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// todo: handle effector
// todo: handle optional renderer
[AddComponentMenu("Scripts/Mifushi 2D/ColoredGameObject")]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class ColoredGameObject : MonoBehaviour
{
    /**
     * Initial color of the game object
     */
    [SerializeField]
    public GameColor initialColor = GameColor.WHITE;

    /**
     * Current color of the game object
     */
    public GameColor Color
    {
        get { return _color; }
        set { SetColor(value);  }
    }

    /**
     * Current layer mask of the game object
     */
    public int GameColorLayerMask
    {
        get { return _colorsManager.GetCombinedLayerMask(Color); }
    }

    /**
     * Delegate for when this game object changes color.
     * Uncomment if needed
     */
    //public delegate void OnColorChangeDelegate(GameColor newColor);
    //public event OnColorChangeDelegate OnColorChangeEvent;

    protected ColorsManager _colorsManager;

    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider2D;
    protected ContactFilter2D _contactFilter2D;

    protected GameColor _color;
    protected Collider2D[] _collider2Ds = new Collider2D[16];
    protected bool _register = true;

    [Inject]
    public virtual void Init(ColorsManager colorsManager)
    {
        Debug.Log("ColoredGameObject.Init " + gameObject.name);
        _colorsManager = colorsManager;
    }

    /**
     * Change the color of this game object
     */
    public virtual bool SetColor(GameColor newColor)
    {
        if (!canChangeColor(newColor)) return false;

        _color = newColor;
        gameObject.layer = _colorsManager.GetLayer(this._color);
        _spriteRenderer.color = _colorsManager.GetColor(newColor);
        //this.OnColorChangeEvent?.Invoke(newColor);
        return true;
    }

    /**
     * Cycle the color of this game object.
     * Goes from Red -> Green -> Blue -> Red ...
     */
    public virtual void CycleColor()
    {
        switch (Color)
        {
            case GameColor.RED:
                SetColor(GameColor.GREEN);
                break;
            case GameColor.GREEN:
                SetColor(GameColor.BLUE);
                break;
            case GameColor.BLUE:
                SetColor(GameColor.RED);
                break;
            default:
                break;
        }
    }

    public virtual void OnWorldColorChange(GameColor worldColor)
    {
        if (Color != worldColor)
        {
            Color newColor = this._spriteRenderer.color;
            newColor.a = 0.5f;
            this._spriteRenderer.color = newColor;
        }
        else
        {
            Color newColor = this._spriteRenderer.color;
            newColor.a = 1f;
            this._spriteRenderer.color = newColor;
        }
    }

    protected virtual bool canChangeColor(GameColor newColor)
    {
        _contactFilter2D.SetLayerMask(_colorsManager.GetLayerMask(newColor));

        int count = _collider2D.OverlapCollider(_contactFilter2D, _collider2Ds);
        Debug.Log("ColoredGameObject#canChangeColor " + gameObject.name + " -> Got  " + count + " Collisions");
        for (int i = 0; i < count; ++i)
        {
            Debug.Log("ColoredGameObject#canChangeColor " + gameObject.name + " -> Collision detected with " + _collider2Ds[i].gameObject.name);
        }
        return count == 0;
    }

    protected virtual void Start()
    {
        Debug.Log("ColoredGameObject.Start " + gameObject.name);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _contactFilter2D = new ContactFilter2D();
        _contactFilter2D.useTriggers = false;
        _contactFilter2D.useLayerMask = true;

        SetColor(initialColor);

        if (_register)
        {
            _colorsManager.RegisterColoredGameObject(this);
            OnWorldColorChange(_colorsManager.WorldColor);
        }
    }

    protected virtual void Update()
    {
    }
}
