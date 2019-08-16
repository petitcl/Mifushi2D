using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Zenject;

[AddComponentMenu("Scripts/Mifushi 2D/InputPlayerController")]
[RequireComponent(typeof(PlayerController))]
public class InputPlayerController : MonoBehaviour
{
    [SerializeField]
    public CrossPlatformInputManager.ActiveInputMethod activeInputMethod = CrossPlatformInputManager.ActiveInputMethod.Touch;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        CrossPlatformInputManager.SwitchActiveInputMethod(activeInputMethod);
    }

    private void Update()
    {
        // update color
        if (CrossPlatformInputManager.GetButtonDown("ColorChange"))
        {
            Debug.LogWarning("CycleColor");
            _playerController.CycleColor();
        }

        // update movement
        float horizontalMove = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        _playerController.Move(horizontalMove);

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Debug.LogWarning("Jump");
            _playerController.Jump();
        }
    }

}
