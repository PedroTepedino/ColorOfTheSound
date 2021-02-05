using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyDummy : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    

    private void OnValidate()
    {
        if (_rigidbody == null)
        {
            _rigidbody = this.GetComponent<Rigidbody>();
        }
    }
}
