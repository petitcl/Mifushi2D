using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIFader : MonoBehaviour, Fader
{
    public bool IsFadedOut { get { return _fadedOut; } }
    
    private Graphic[] _graphics;
    private bool _fadedOut = true;
 
    public void Awake()
    {
        _graphics = GetComponentsInChildren<Graphic>();
        if (_graphics.Length > 0) {
            _fadedOut = _graphics[0].color.a == 0.0f;
        }
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
        Debug.Log(String.Format("{0} Show", gameObject.name));
        iTween.Stop(gameObject, true);
        gameObject.SetActive(true);
        SetAlpha(1.0f);
        _fadedOut = false;
    }

    public void Hide()
    {
        Debug.Log(String.Format("{0} Hide", gameObject.name));
        iTween.Stop(gameObject, true);
        SetAlpha(0.0f);
        gameObject.SetActive(false);
        _fadedOut = true;
    }

    public void FadeIn()
    {
        Debug.Log(String.Format("{0} FadeIn", gameObject.name));
        if (!_fadedOut)
        {
            return;
        }
        iTween.Stop(gameObject, true);
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
        Debug.Log(String.Format("{0} FadeOut", gameObject.name));
        if (_fadedOut)
        {
            return;
        }
        iTween.Stop(gameObject, true);
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
