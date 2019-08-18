using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScreenTapPlayerJoystick : MonoBehaviour
{
    private void Update()
    {
        Debug.Log("Update");
        if (Input.touchCount == 0)
        {
            UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.SetAxis("Horizontal", 0.0f);
            return;
        }
        Touch touch = Input.GetTouch(0);
        float screenWidth = Screen.width;
        if (touch.position.x > (Screen.width / 2))
        {
            UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.SetAxis("Horizontal", 1.0f);
        }
        else
        {
            UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.SetAxis("Horizontal", -1.0f);
        }
    }
}
