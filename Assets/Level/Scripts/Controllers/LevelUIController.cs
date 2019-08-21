using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// todo allow to disable buttons entirely
public class LevelUIController : MonoBehaviour
{
    public Fader Overlay { get { return _overlay; } } 
    public CustomButton ColorChangeButton { get { return _colorChangeButton; } } 
    public MobilePlayerJoytick Joystick { get { return _joystick; } } 

    [SerializeField]
    private UIFader _overlay;

    [SerializeField]
    private MobilePlayerJoytick _joystick;

    [SerializeField]
    private CustomButton _colorChangeButton;

    public void ShowControls()
    {
        Joystick.Enable();
        ColorChangeButton.Enable();
    }

    public void HideControls()
    {
       Joystick.Fader.Hide();
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
