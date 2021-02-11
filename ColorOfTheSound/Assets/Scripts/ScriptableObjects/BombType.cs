using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewBombType", menuName = "ObjectTypes/Bombs", order = 1)]
public class BombType : ScriptableObject
{
    [BoxGroup("Explosion Parameters", CenterLabel = true), PropertyRange(0.1f, 10f)]
    [SerializeField] private float _radius = 2f;
    
    [BoxGroup("Explosion Parameters")] 
    [SerializeField] private float _timeToExplode = 5f;

    public float Radius => _radius;
    public float TimeToExplode => _timeToExplode;
}
