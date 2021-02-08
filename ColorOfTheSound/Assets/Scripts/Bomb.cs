using System;
using UnityEngine;

public class Bomb : MonoBehaviour, IPoolableObject
{
    [Header("Parameters")]
    [SerializeField] private float _radius = 2f;

    [SerializeField] private float _timeToExplode = 5f;
    private float _timer = 0f;

    public void OnSpawn()
    {
        Debug.Log("OiGato");
        _timer = _timeToExplode;
    }

    private void Update()
    {
        if (_timer >= 0f) 
            _timer -= Time.deltaTime;
        else
            Explode();
    }

    private void Explode()
    {
        Debug.Log("Explode");
        this.gameObject.SetActive(false);
    }

    public GameObject ThisGameObject => this.gameObject;
    public bool IsActiveInHierarchy => this.gameObject.activeInHierarchy;
    
    
}
