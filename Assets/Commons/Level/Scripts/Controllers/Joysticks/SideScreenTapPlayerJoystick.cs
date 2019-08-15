using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScreenTapPlayerJoystick : PlayerJoystick
{
    public float GetHorizontalAxis()
    {
        if (Input.touchCount == 0)
        {
            return 0.0f;
        }
        Touch touch = Input.GetTouch(0);
        float screenWidth = Screen.width;
        if (touch.position.x > (Screen.width / 2))
        {
            return 1.0f;
        }
        else
        {
            return -1.0f;
        }
    }

    public bool IsChangingColor()
    {
        return false;
    }

    public bool IsJumping()
    {
        return false;
    }
}
