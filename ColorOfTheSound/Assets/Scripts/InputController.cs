using System;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController 
{
    private PlayerControlScheme _controls;
    
    public event Action<Vector2> OnMove;
    public event Action OnBasicAttack;
    public event Action OnStunAttack;
    
    
    

    public InputController()
    {
        Controls = new PlayerControlScheme();
        
        SubscribeFunctions();        
    }

    public PlayerControlScheme Controls
    {
        get => _controls;
        set => _controls = value;
    }

    public void EnableGameplayInput()
    {
        Controls.Gameplay.Enable();
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
    
    private void OnBasicAttackInput(InputAction.CallbackContext context)
    {
        OnBasicAttack?.Invoke();
    }

    protected virtual void OnBasicStunAttackInput(InputAction.CallbackContext context)
    {
        OnStunAttack?.Invoke();
    }

    private void SubscribeFunctions()
    {
        Controls.Gameplay.Move.performed += OnMoveInput;
        Controls.Gameplay.Move.canceled += OnMoveInput;
        
        Controls.Gameplay.BasicAttack.performed += OnBasicAttackInput;
        
        Controls.Gameplay.StunAttack.performed += OnBasicStunAttackInput;
    }
}
