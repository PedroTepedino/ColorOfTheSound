using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Mover _mover;
    private Attacker _attacker;
    
    [SerializeField] private Rigidbody _rigidbody;
    public Rigidbody Body => _rigidbody;
    
    
    [SerializeField] private float _movementSpeed = 5f;
    public float MovementSpeed => _movementSpeed;

    public Vector3 RelativePosition { get => _relativePosition; set => _relativePosition = value; }
    public float Radius { get => _radius; set => _radius = value; }
    public GameObject BasicAttackParticleSystem => _basicAttackParticleSystem;
    public float PushForce => _pushForce;

    [Header("Attacks")]
    [Space]
    [Header("Basic Attack")]
    [SerializeField] private float _radius = 1.0f;
    [SerializeField] private Vector3 _relativePosition = new Vector3(1f,0f, 0f);
    [SerializeField] private GameObject _basicAttackParticleSystem;
    [SerializeField] private float _pushForce = 10f;

    private void Awake()
    {
        _mover = new Mover(this);
        _attacker = new Attacker(this);
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

public class Attacker : IAttackAction
{
    private readonly Transform _playerTransform;
    private readonly float _radius;
    private readonly Vector3 _relativePosition;
    private readonly GameObject _particleSystem;
    private readonly float _pushForce;
    
    public Attacker(Player player)
    {
        _playerTransform = player.transform;
        _radius = player.Radius;
        _relativePosition = player.RelativePosition;
        _pushForce = player.PushForce;
        _particleSystem = player.BasicAttackParticleSystem;
        _particleSystem.SetActive(false);
    }
    
    public void OnAttack()
    {
        _particleSystem.SetActive(true);

        Collider[] colliders = new Collider[10];
        int objectsHitCount = Physics.OverlapSphereNonAlloc(_playerTransform.position + _relativePosition, _radius, colliders);

        for (int i = 0; i < objectsHitCount; i++)
        {
            // Check if the colliders object has an IPushable, if it HAS HIT IT!
            IPushable objectToPush = colliders[i].GetComponent<IPushable>();
            
            if (objectToPush != null)
            {
                Vector3 force = colliders[i].transform.position - _playerTransform.position;
                force = force.normalized * _pushForce;
                
                objectToPush.Push(force);
            }
        }
    }
}

public interface IAttackAction
{
    void OnAttack();
}

public interface IHittable
{
    void Hit();
}

public interface IPushable
{
    void Push(Vector3 force);
}
