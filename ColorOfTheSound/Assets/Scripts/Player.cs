using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEditor.SceneTemplate;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private bool _viewGizmos = true;
    
    private IMover _mover;
    private BasicAttacker _basicAttacker;
    private StunAttacker _stunAttacker;
 
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    
    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 5f;

    [Header("Basic Attack")]
    [SerializeField] private float _basicAttackRadius = 1.0f;
    [SerializeField] private float _attackDistance = 1f;
    [SerializeField] private ParticleSystem _basicAttackParticleSystem;
    [SerializeField] private float _pushForce = 10f;

    [Header("Stun AOE Attack")] 
    [SerializeField] private float _stunAttackRadius = 5f;
    [SerializeField] private float _stunTime = 2f;
    [SerializeField] private ParticleSystem _stunAttackParticleSystem;

    #region Properties
    
    public Rigidbody Body => _rigidbody;
    public float MovementSpeed => _movementSpeed;
    public float BasicAttackRadius { get => _basicAttackRadius; set => _basicAttackRadius = value; }
    public ParticleSystem BasicAttackParticleSystem => _basicAttackParticleSystem;
    public ParticleSystem StunAttackParticleSystem => _stunAttackParticleSystem;
    public float PushForce => _pushForce;
    public float AttackDistance { get => _attackDistance; set => _attackDistance = value; }
    public float StunAttackRadius { get => _stunAttackRadius; set => _stunAttackRadius = value; }
    public float StunTime => _stunTime;
    public bool ViewGizmos => _viewGizmos;
    
    #endregion

    private void Awake()
    {
        _mover = new Mover(this);
        _basicAttacker = new BasicAttacker(this);
        _stunAttacker = new StunAttacker(this);

        GameManager.Instance.Controller.Controls.Gameplay.BombAttack.started += OnBomb;
    }

    private static List<BombType> _parametes;
    
    

    private void Update()
    {
        Debug.Log($"mover = {_mover}");
    }

    private void OnBomb(InputAction.CallbackContext obj)
    {
        Debug.Log("Bomb Input");
    }

    private void FixedUpdate()
    {
        _mover?.Tick();
    }

    private IMover ChangeMoverType<T>() where T : IMover
    {
        if (typeof(T) == typeof(Dasher))
            return new Dasher(this);
        else
            return new Mover(this);
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
        {
            _rigidbody = this.GetComponent<Rigidbody>();
        }
    }
}

public class Dasher : IMover
{
    public bool Teste = true;
    public Dasher(Player player)
    {
        
    }
    
    public void Tick()
    {
        
    }
}