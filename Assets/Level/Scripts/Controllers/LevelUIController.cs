using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// todo allow to disable buttons entirely
public class LevelUIController : MonoBehaviour
{
    public Fader Overlay { get { return _overlay; } } 
    public ColorChangeButton ColorChangeButton { get { return _colorChangeButton; } } 
    public MobilePlayerJoytick Joystick { get { return _joystick; } } 

    [SerializeField]
    private UIFader _overlay;

    [SerializeField]
    private MobilePlayerJoytick _joystick;

    [SerializeField]
    private ColorChangeButton _colorChangeButton;

    public void ShowControls()
    {
        Joystick.Enable();
        ColorChangeButton.Button.Enable();
    }

    public void HideControls()
    {
       Joystick.Fader.Hide();
       Debug.Log(String.Format("{0}, {1}", ColorChangeButton != null, ColorChangeButton?.Fader != null));
       ColorChangeButton.Fader.Hide();
    }

    public void FadeOut()
    {
        Joystick.Fader.FadeOut();
        ColorChangeButton.Fader.FadeOut();
    }

    public void FadeIn()
    {
        Joystick.Fader.FadeIn();
        ColorChangeButton.Fader.FadeIn();
    }
}
