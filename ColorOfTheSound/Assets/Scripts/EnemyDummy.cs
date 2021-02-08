using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyDummy : MonoBehaviour, IPushable, IStunable
{
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private ParticleSystem _stunParticleSystem;
    private Coroutine _stunCoroutine;

    private bool _stunned = false;

    private void OnEnable()
    {
        _stunParticleSystem.Stop();
        _stunCoroutine = null;
    }

    public void Push(Vector3 force)
    {
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }
    
    public void Stun(float stunTime)
    {
        if (_stunCoroutine != null || _stunned) return;

        _stunParticleSystem.Play();
        _stunCoroutine = StartCoroutine(StunTimer(stunTime));
    }

    private IEnumerator StunTimer(float stunTime)
    {
        _stunned = true;
        
        yield return new WaitForSeconds(stunTime);

        _stunned = false;
        _stunParticleSystem.Stop();
        _stunCoroutine = null;
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
        {
            _rigidbody = this.GetComponent<Rigidbody>();
        }
    }
}
