using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CustomButton : Button, IPointerUpHandler
{
    public bool IsButtonPressed { get { return interactable && IsPressed(); } }
    public Fader Fader { get { return _fader; } }
    
    [SerializeField]
    public ButtonClickedEvent OnPressed;

    private UIFader _fader;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (interactable) {
            OnPressed?.Invoke();
        }
    }

    public void Enable()
    {
        interactable = true;
    }

    public void Disable()
    {
        interactable = false;
    }

    private void AddFaderImageIfNeeded()
    {
        if (_fader == null)
        {
            gameObject.AddComponent<UIFader>();
            _fader = GetComponent<UIFader>();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _fader = GetComponent<UIFader>();
        AddFaderImageIfNeeded();
    }
}