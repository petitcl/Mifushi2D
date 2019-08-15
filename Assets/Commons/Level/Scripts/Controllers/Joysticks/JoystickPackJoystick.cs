using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickPackJoystick : MonoBehaviour, PlayerJoystick
{
    public enum JumpInput
    {
        Joystick,
        JoystickReset,
        Button
    }

    [SerializeField]
    public Joystick joystick;

    [SerializeField]
    public PressButton jumpButton;

    [SerializeField]
    public PressButton colorChangedButton;

    [SerializeField]
    public JumpInput jumpInput;

    private bool _jumpButtonPressed = false;
    private bool _colorChangeButtonPressed = false;
    private bool _verticalAxisPressed = false;
    private bool _verticalAxisReset = true;

    public void OnJumpButtonPressed()
    {
        _jumpButtonPressed = true;
    }

    public void OnColorChangeButtonPressed()
    {
        _colorChangeButtonPressed = true;
    }

    public float GetHorizontalAxis()
    {
        return joystick.Horizontal;
    }

    public bool IsChangingColor()
    {
        return _colorChangeButtonPressed;
    }

    public bool IsJumping()
    {
        if (jumpInput == JumpInput.Joystick)
        {
            return joystick.Vertical > 0;
        }
        else if (jumpInput == JumpInput.JoystickReset)
        {
            return _verticalAxisPressed;
        }
        else
        {
            return _jumpButtonPressed;
        }
    }

    private void Start()
    {
        jumpButton.OnPressed.AddListener(this.OnJumpButtonPressed);
        colorChangedButton.OnPressed.AddListener(this.OnColorChangeButtonPressed);
    }

    private void Update()
    {
        //Debug.Log("Update " + joystick.Vertical + " " + _verticalAxisPressed + " " + _verticalAxisReset);
        if (joystick.Vertical <= 0) _verticalAxisReset = true;
        if (joystick.Vertical > 0 && !_verticalAxisPressed && _verticalAxisReset)
        {
            _verticalAxisPressed = true;
            _verticalAxisReset = false;
        }
        //Debug.Log("AfterUpdate " + joystick.Vertical + " " + _verticalAxisPressed + " " + _verticalAxisReset);
    }

    private void LateUpdate()
    {
        if (_jumpButtonPressed) _jumpButtonPressed = false;
        if (_colorChangeButtonPressed) _colorChangeButtonPressed = false;
        if (_verticalAxisPressed) _verticalAxisPressed = false;
    }
}
