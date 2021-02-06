using UnityEngine;

public class StunAttacker : IAttackAction
{
    private readonly Transform _playerTransform;
    private readonly float _radius;
    private readonly float _stunTime;
    public StunAttacker(Player player)
    {
        _playerTransform = player.transform;
        _radius = player.StunAttackRadius;
        _stunTime = player.StunTime;
    }
    
    public void OnAttack()
    {
        Collider[] colliders = new Collider[10];
        int hitCount = Physics.OverlapSphereNonAlloc(_playerTransform.position, _radius, colliders);

        for (int i = 0; i < hitCount; i++)
        {
            colliders[i].GetComponent<IStunable>()?.Stun(_stunTime);
        }
    }
}