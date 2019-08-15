using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPlayerJoystick : PlayerJoystick
{
    public float GetHorizontalAxis()
    {
        // update movement
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            return 1.0f;
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            return -1.0f;
        }
        else
        {
            return 0.0f;
        }
    }

    public bool IsChangingColor()
    {
        return Input.GetMouseButtonDown(1);
    }

    public bool IsJumping()
    {
        return Input.GetKeyDown("space");
    }
}
