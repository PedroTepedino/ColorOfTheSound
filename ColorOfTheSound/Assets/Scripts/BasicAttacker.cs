using UnityEngine;

public class BasicAttacker : IAttackAction
{
    private readonly Transform _playerTransform;
    private readonly float _radius;
    private readonly float _attackDistance;
    private readonly ParticleSystem _particleSystem;
    private readonly float _pushForce;
    
    public BasicAttacker(Player player)
    {
        _playerTransform = player.transform;
        _radius = player.BasicAttackRadius;
        _attackDistance = player.AttackDistance;
        _pushForce = player.PushForce;
        _particleSystem = player.BasicAttackParticleSystem;
        
        _particleSystem.Stop();

        GameManager.Instance.Controller.OnBasicAttack += OnAttack;
    }
    
    public void OnAttack()
    {
        _particleSystem.Play();
        Collider[] colliders = new Collider[10];
        int objectsHitCount = Physics.OverlapSphereNonAlloc(_playerTransform.position + (_playerTransform.right * _attackDistance), _radius, colliders);

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