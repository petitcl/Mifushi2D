using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ColorChangeButton : MonoBehaviour
{
    public Fader Fader { get { return _fader; } }
    public CustomButton Button { get { return _button; } }

    public bool Animate { get; set; } = true;

    [SerializeField]
    private CustomButton _button;

    [SerializeField]
    private UIFader _fader;

    [SerializeField]
    private Image _currentColorImage;

    [SerializeField]
    private Image _nextColorImage;

    private SignalBus _signalBus;
    private ColorsManager _colorsManager;

    [Inject]
    public void Init(SignalBus signalBus, ColorsManager colorsManager)
    {
        _signalBus = signalBus;
        _colorsManager = colorsManager;
    }

    public void OnPlayerChangedColor(PlayerChangedColorSignal signal)
    {
        GameColor color = signal.newColor;
        if (Animate || signal.oldColor == GameColor.NONE)
        {
            Refresh(color, Animate && signal.oldColor != GameColor.NONE);
        }
    }

    public void Refresh(GameColor color, bool animate = true)
    {
        Color newCurrentColor = _colorsManager.GetColor(color);
        Color newNextColor = _colorsManager.GetColor(_colorsManager.GetNextColor(color));

        if (animate)
        {
            iTween.ValueTo(gameObject, iTween.Hash(
                "from", _currentColorImage.color,
                "to", newCurrentColor,
                "onupdate", "SetCurrentColorImageColor",
                "time", 1.0f,
                "easetype", iTween.EaseType.easeOutCubic
            ));
             iTween.ValueTo(gameObject, iTween.Hash(
                "from", _nextColorImage.color,
                "to", newNextColor,
                "onupdate", "SetNextColorImageColor",
                "time", 1.0f,
                "easetype", iTween.EaseType.easeOutCubic
            ));
        }
        else
        {
            _currentColorImage.color = newCurrentColor;
            _nextColorImage.color = newNextColor;
        }
    }

    private void SetCurrentColorImageColor(Color color)
    {
        _currentColorImage.color = color;
    }

    private void SetNextColorImageColor(Color color)
    {
        _nextColorImage.color = color;
    }

    private void Awake()
    {
        _signalBus.Subscribe<PlayerChangedColorSignal>(s => this.OnPlayerChangedColor(s));
    }
}
