using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MobilePlayerJoytick : MonoBehaviour
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
        if (jumpInput == JumpInput.Button)
        {
            CrossPlatformInputManager.SetButtonDown("Jump");
            _jumpButtonPressed = true;
        }
    }

    public void OnColorChangeButtonPressed()
    {
        CrossPlatformInputManager.SetButtonDown("ColorChange");
        _colorChangeButtonPressed = true;
    }

    private void Start()
    {
        if (jumpInput == JumpInput.Button)
        {
            jumpButton.gameObject.SetActive(true);
            jumpButton.OnPressed.AddListener(this.OnJumpButtonPressed);
        }
        else
        {
            jumpButton.gameObject.SetActive(false);
        }

        colorChangedButton.OnPressed.AddListener(this.OnColorChangeButtonPressed);
    }

    private void Update()
    {
        //Debug.Log("Update " + joystick.Vertical + " " + _verticalAxisPressed + " " + _verticalAxisReset);
        if (joystick.Vertical <= 0)
        {
            _verticalAxisReset = true;
        }
        if (joystick.Vertical > 0 && !_verticalAxisPressed && _verticalAxisReset)
        {
            _verticalAxisPressed = true;
            _verticalAxisReset = false;
            CrossPlatformInputManager.SetButtonDown("Jump");
        }
        if (jumpInput == JumpInput.Joystick)
        {
            if (joystick.Vertical > 0)
            {
                CrossPlatformInputManager.SetButtonDown("Jump");
            }
        }
        CrossPlatformInputManager.SetAxis("Horizontal", joystick.Horizontal);
    }

    private void LateUpdate()
    {
        // in all cases, reset jump button
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            CrossPlatformInputManager.SetButtonUp("Jump");
        }
        if (_jumpButtonPressed)
        {
            _jumpButtonPressed = false;
        }
        if (_colorChangeButtonPressed)
        {
            CrossPlatformInputManager.SetButtonUp("ColorChange");
            _colorChangeButtonPressed = false;
        }
        if (_verticalAxisPressed)
        {
            _verticalAxisPressed = false;
        }
    }
}
