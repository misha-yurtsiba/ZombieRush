using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Action<Vector2> playerTouched;

    private PlayerInput playerInput;
    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Gameplay.Enable();
        playerInput.Gameplay.PlayerTouch.performed += PlayerTouch;
    }

    private void OnDisable()
    {
        playerInput.Gameplay.Disable();
        playerInput.Gameplay.PlayerTouch.performed -= PlayerTouch;
    }

    private void PlayerTouch(InputAction.CallbackContext context)
    {
        playerTouched?.Invoke(playerInput.Gameplay.TouchPosition.ReadValue<Vector2>());     
    }
}
