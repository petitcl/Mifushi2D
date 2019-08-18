using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Zenject;

[AddComponentMenu("Scripts/Mifushi 2D/InputPlayerController")]
[RequireComponent(typeof(PlayerController))]
public class InputPlayerController : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        // update color
        if (CrossPlatformInputManager.GetButtonDown("ColorChange"))
        //if (Input.GetButtonDown("ColorChange"))
        {
            Debug.Log("CycleColor");
            _playerController.CycleColor();
        }

        // update movement
        float horizontalMove = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        //float horizontalMove = Input.GetAxisRaw("Horizontal");

        _playerController.Move(horizontalMove);

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        // if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            _playerController.Jump();
        }
    }

}
