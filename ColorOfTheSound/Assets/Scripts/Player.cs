using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Mover _mover;
    
    [SerializeField] private Rigidbody _rigidbody;
    public Rigidbody Body => _rigidbody;

    #region Parapeters

    [SerializeField] private float _movementSpeed = 5f;
    public float MovementSpeed => _movementSpeed;

    #endregion

    private void Awake()
    {
        _mover = new Mover(this);
    }
    
    private void FixedUpdate()
    {
        _mover.Tick();
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
        {
            _rigidbody = this.GetComponent<Rigidbody>();
        }
    }
}