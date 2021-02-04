using UnityEngine;

public class Mover
{
    private readonly Rigidbody _rigidBody;
    private readonly Transform _playerTransform;

    private Vector3 _moveInput = Vector3.zero;

    private readonly float _speedMultiplier;
    
    public Mover(Player player)
    {
        _rigidBody = player.Body;
        _speedMultiplier = player.MovementSpeed;
        _playerTransform = player.transform;
        
        GameManager.Instance.Controller.OnMove += ListenOnMove;
    }

    private void ListenOnMove(Vector2 moveInput)
    {
        // transform 2d vector into 3D with the Y axis Up
        _moveInput.x = moveInput.x;
        _moveInput.z = moveInput.y;
    }
    
    public void Tick()
    {
        Vector3 currentTickInput = _moveInput;
        
        _rigidBody.velocity = GetCurrentVelocity(currentTickInput);
        
        if (currentTickInput.magnitude < 0.1f)
            return;
        
        _playerTransform.rotation = GetRotation(currentTickInput);
    }

    private Vector3 GetCurrentVelocity(Vector3 input)
    {
        Vector3 currentVelocity = _moveInput.normalized * _speedMultiplier;
        currentVelocity.y = _rigidBody.velocity.y;
        return currentVelocity;
    }

    private Quaternion GetRotation(Vector3 input)
    {
        return Quaternion.Euler(0, - Mathf.Atan2(input.z, input.x) * Mathf.Rad2Deg,0);
    }
}