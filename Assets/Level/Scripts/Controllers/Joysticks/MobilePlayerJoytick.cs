using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(UIFader))]
public class MobilePlayerJoytick : MonoBehaviour
{
    public Fader Fader { get { return _fader; } }

    [SerializeField]
    public Joystick joystick;

    private UIFader _fader;
    private bool _colorChangeButtonPressed = false;
    private bool _verticalAxisPressed = false;
    private bool _verticalAxisReset = true;

    public void Enable()
    {
        Debug.Log("MobilePlayerJoytick.Enable");
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        Debug.Log("MobilePlayerJoytick.Disable");
        gameObject.SetActive(false);
    }

    public void OnColorChangeButtonPressed()
    {
        CrossPlatformInputManager.SetButtonDown("ColorChange");
        _colorChangeButtonPressed = true;
    }

    private void Awake()
    {
        _fader = GetComponent<UIFader>();
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
        CrossPlatformInputManager.SetAxis("Horizontal", joystick.Horizontal);
    }

    private void LateUpdate()
    {
        // in all cases, reset jump button
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            CrossPlatformInputManager.SetButtonUp("Jump");
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
