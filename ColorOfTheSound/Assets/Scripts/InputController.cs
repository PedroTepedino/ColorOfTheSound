using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PlayerControlScheme _controls;

    private void Awake()
    {
        _controls = new PlayerControlScheme();
    }

    private void OnEnable()
    {
        _controls.Gameplay.Enable();
        _controls.Gameplay.Move.performed += OnMove;
    }
    
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (moveInput.magnitude > 1.0f)
        {
            moveInput.Normalize();
        }

        Debug.Log($"Move input: {moveInput}");
    }
}
