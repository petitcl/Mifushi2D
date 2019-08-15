using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerJoystick
{
    float GetHorizontalAxis();

    bool IsJumping();

    bool IsChangingColor();

}
