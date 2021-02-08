using UnityEngine;

public interface IPoolableObject
{
    public GameObject ThisGameObject { get; }
    public bool IsActiveInHierarchy { get; }
    public void OnSpawn();
}