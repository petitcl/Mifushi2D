using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIController : MonoBehaviour
{
    [SerializeField]
    public FadingImage overlay;

    [SerializeField]
    public GameObject joystick;

    [SerializeField]
    public GameObject colorChangeButton;

    //[SerializeField]
    //public GameObject jumpButton;

    public void ShowControls()
    {
        joystick.SetActive(true);
        ShowColorChangeButton();
        //jumpButton.SetActive(true);
    }

    public void HideControls()
    {
        joystick.SetActive(false);
        HideColorChangeButton();
        //jumpButton.SetActive(false);
    }

    public void ShowColorChangeButton()
    {
        colorChangeButton.SetActive(true);
    }

    public void HideColorChangeButton()
    {
        colorChangeButton.SetActive(false);
    }

    public void HideOverlay()
    {
        overlay.Hide();
    }

    public void ShowOverlay()
    {
        overlay.Show();
    }

    public void OverlayFadeIn()
    {
        overlay.FadeIn();
    }

    public void OverlayFadeOut()
    {
        overlay.FadeOut();
    }

}
