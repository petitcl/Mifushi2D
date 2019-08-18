using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Zenject;

[AddComponentMenu("Scripts/Mifushi 2D/InputPlayerController")]
[RequireComponent(typeof(PlayerController))]
public class InputPlayerController : MonoBehaviour
{
    [SerializeField]
    public bool forceDesktopControls = false;
    public bool CanMove { get; set; }  = true;

    private PlayerController _playerController;

    private void Start()
    {
        if (forceDesktopControls)
        {
            CrossPlatformInputManager.SwitchActiveInputMethod(CrossPlatformInputManager.ActiveInputMethod.Hardware);
        }
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        // update color
        if (CrossPlatformInputManager.GetButtonDown("ColorChange") && CanMove)
        {
            Debug.Log("CycleColor");
            _playerController.CycleColor();
        }

        // update movement
        float horizontalMove = CanMove ? CrossPlatformInputManager.GetAxisRaw("Horizontal") : 0.0f;
        //float horizontalMove = Input.GetAxisRaw("Horizontal");

        _playerController.Move(horizontalMove);

        if (CrossPlatformInputManager.GetButtonDown("Jump") && CanMove)
        // if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            _playerController.Jump();
        }
    }

}
