using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIFader : MonoBehaviour, Fader
{
    public bool IsFadedOut { get { return _fadedOut; } }
    
    private Graphic[] _graphics;
    private bool _fadedOut = false;
 
    public void Awake()
    {
        _graphics = GetComponentsInChildren<Graphic>();
    }

    public void SetAlpha(float alpha)
    {
        foreach (Graphic graphic in _graphics)
        {
            Color color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        SetAlpha(1.0f);
        _fadedOut = false;
    }

    public void Hide()
    {
        SetAlpha(0.0f);
        gameObject.SetActive(false);
        _fadedOut = true;
    }

    public void FadeIn()
    {
        if (!_fadedOut)
        {
            return;
        }
        gameObject.SetActive(true);
        SetAlpha(0.0f);
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0.0f,
            "to", 1.0f,
            "onupdate", "SetAlpha",
            "time", 2.0f,
            "easetype", iTween.EaseType.easeOutCubic
        ));
        _fadedOut = false;
    }

    public void FadeOut()
    {
        if (_fadedOut)
        {
            return;
        }
        SetAlpha(1.0f);
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 1.0f,
            "to", 0.0f,
            "onupdate", "SetAlpha",
            "oncomplete", "SetActive",
            "oncompletetarget", gameObject,
            "oncompleteparams", false,
            "time", 1.0f,
            "easetype", iTween.EaseType.easeOutCubic
        ));
         _fadedOut = true;
    }

}
