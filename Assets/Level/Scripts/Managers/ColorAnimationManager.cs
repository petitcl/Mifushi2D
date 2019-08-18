using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: have only one global instance of this class
public class ColorAnimationManager
{
    public float Value { get { return _value; } }
    public bool IsTransitioning { get { return _isTransitioning; } }

    private float _animationDuration = 0.5f;
    private bool _isTransitioning = false;
    private float _currentTime = 0.0f;
    private float _value = 0.0f;
    private float _percentage = 0.0f;

    public void StartAnimation()
    {
        _isTransitioning = true;
        _percentage = 0.0f;
        _value = 0.0f;
        _currentTime = 0.0f;
    }

    public void Update()
    {
        if (!_isTransitioning) return;
        if (_percentage >= 1.0f)
        {
            _isTransitioning = false;
            _value = 0.0f;
            _currentTime = 0.0f;
        }
        _currentTime += Time.deltaTime;
        _percentage = _currentTime / _animationDuration;
        _value = Easing.Exponential.Out(_percentage);
        //_value = Easing.Instant(_percentage);
    }


}
