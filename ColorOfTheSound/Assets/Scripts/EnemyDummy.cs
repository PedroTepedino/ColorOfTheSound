using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyDummy : MonoBehaviour, IPushable
{
    [SerializeField] private Rigidbody _rigidbody;


    public void Push(Vector3 force)
    {
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
        {
            _rigidbody = this.GetComponent<Rigidbody>();
        }
    }
}
