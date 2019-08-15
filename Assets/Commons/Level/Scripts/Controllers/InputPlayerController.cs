using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[AddComponentMenu("Scripts/Mifushi 2D/InputPlayerController")]
[RequireComponent(typeof(PlayerController))]
public class InputPlayerController : MonoBehaviour
{
    private PlayerJoystick _playerJoystick;

    private PlayerController _playerController;

    [Inject]
    public void Init(PlayerJoystick playerJoystick)
    {
        _playerJoystick = playerJoystick;
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        // update color
        if (_playerJoystick.IsChangingColor())
        {
            _playerController.CycleColor();
        }

        // update movement
        float horizontalMove = _playerJoystick.GetHorizontalAxis();
        _playerController.Move(horizontalMove);

        if (_playerJoystick.IsJumping())
        {
            Debug.LogWarning("Jump");
            _playerController.Jump();
        }
    }

}
