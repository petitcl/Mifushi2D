using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadingImage : MonoBehaviour
{
    private Image _image;

    public void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetAlpha(float alpha)
    {
        Color color = _image.color;
        color.a = alpha;
        _image.color = color;
    }

    public void Show()
    {
        SetAlpha(1.0f);
    }

    public void Hide()
    {
        SetAlpha(0.0f);
    }

    public void FadeIn()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 1.0f,
            "to", 0.0f,
            "onupdate", "SetAlpha",
            "time", 1.0f,
            "easetype", iTween.EaseType.easeOutCubic
        ));
    }

    public void FadeOut()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0.0f,
            "to", 1.0f,
            "onupdate", "SetAlpha",
            "time", 1.0f,
            "easetype", iTween.EaseType.easeOutCubic
        ));
    }

}
