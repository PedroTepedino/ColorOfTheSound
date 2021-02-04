using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController 
{
    private PlayerControlScheme _controls;
    
    public event Action<Vector2> OnMove;
    

    public InputController()
    {
        _controls = new PlayerControlScheme();
        
        SubscribeFunctions();        
    }

    public void EnableGameplayInput()
    {
        _controls.Gameplay.Enable();
    }
    
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (moveInput.magnitude > 1.0f)
        {
            moveInput.Normalize();
        }
        
        OnMove?.Invoke(moveInput);
    }

    private void SubscribeFunctions()
    {
        _controls.Gameplay.Move.performed += OnMoveInput;
        _controls.Gameplay.Move.canceled += OnMoveInput;
    }
}
