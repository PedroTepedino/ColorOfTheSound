using UnityEngine;

public class StunAttacker : IAttackAction
{
    private readonly Transform _playerTransform;
    private readonly float _radius;
    private readonly float _stunTime;
    private readonly ParticleSystem _particleSystem;

    public StunAttacker(Player player)
    {
        _playerTransform = player.transform;
        _radius = player.StunAttackRadius;
        _stunTime = player.StunTime;
        _particleSystem = player.StunAttackParticleSystem;

        _particleSystem.Stop();

        GameManager.Instance.Controller.OnStunAttack += OnAttack;
    }
    
    public void OnAttack()
    {
        _particleSystem.Play();
        
        Collider[] colliders = new Collider[10];
        int hitCount = Physics.OverlapSphereNonAlloc(_playerTransform.position, _radius, colliders);

        for (int i = 0; i < hitCount; i++)
        {
            colliders[i].GetComponent<IStunable>()?.Stun(_stunTime);
        
            colliders[i].GetComponent<IHittable>()?.Hit(1);
        }
    }
}