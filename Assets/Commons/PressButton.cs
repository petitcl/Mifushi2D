using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressButton : Button, IPointerUpHandler
{
    public bool IsButtonPressed { get { return IsPressed(); } }

    [SerializeField]
    public ButtonClickedEvent OnPressed;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        OnPressed?.Invoke();
    }

}