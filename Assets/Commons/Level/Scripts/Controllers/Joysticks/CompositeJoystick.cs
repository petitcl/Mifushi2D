using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeJoystick : PlayerJoystick
{
    private PlayerJoystick[] _playerJoysticks;
        
    public CompositeJoystick(params PlayerJoystick[] playerJoystick)
    {
        _playerJoysticks = playerJoystick;
    }

    public float GetHorizontalAxis()
    {
        foreach (PlayerJoystick joystick in _playerJoysticks)
        {
            if (joystick.GetHorizontalAxis() != 0.0f) return joystick.GetHorizontalAxis();
        }
        return 0.0f;
    }

    public bool IsChangingColor()
    {
        foreach (PlayerJoystick joystick in _playerJoysticks)
        {
            if (joystick.IsChangingColor()) return joystick.IsChangingColor();
        }
        return false;
    }

    public bool IsJumping()
    {
        foreach (PlayerJoystick joystick in _playerJoysticks)
        {
            if (joystick.IsJumping()) return joystick.IsJumping();
        }
        return false;
    }
}
