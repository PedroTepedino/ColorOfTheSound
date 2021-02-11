using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class PrefabPool
{
    [HorizontalGroup("Non-Listed", 80)] [PreviewField(80, ObjectFieldAlignment.Left)] [ValidateInput("GameObjectCheck",  "MISSING IPoolableObject !")] [HideLabel] [AssetsOnly]
    public GameObject Prefab;
    [HorizontalGroup("Non-Listed")] [BoxGroup("Non-Listed/Properties", false)] [ValidateInput("TagCheck", "Tag Cannot be Null")]
    public string Tag;
    [HorizontalGroup("Non-Listed")] [BoxGroup("Non-Listed/Properties", false)] [ValidateInput("ObjCountCheck", "Object count cannot be negative!")]
    public int ObjCount = 1;
    [HorizontalGroup("Non-Listed")] [BoxGroup("Non-Listed/Properties", false)]
    public bool IsExpandable = true;

    private bool GameObjectCheck() => Prefab != null && Prefab.GetComponent<IPoolableObject>() != null; 
    private bool TagCheck() => Tag.Length > 0;
    private bool ObjCountCheck() => ObjCount >= 0;
}

public class ObjectPool
{
    public readonly string Tag;
    public readonly GameObject Prefab;
    public readonly bool IsExpandable ;

    public Queue<GameObject> Pool;

    public ObjectPool(PrefabPool _prefab)
    {
        Tag = _prefab.Tag;
        Prefab = _prefab.Prefab;
        IsExpandable = _prefab.IsExpandable;

        Pool = new Queue<GameObject>();
        
        PopulatePool(_prefab.ObjCount);
    }

    private void PopulatePool(int initialObjectCount = 0)
    {
        for (int i = 0; i < initialObjectCount; i++)
        {
            var obj = CreateObject();
            
            obj.SetActive(false);
            
            Pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        GameObject obj = null;
        
        if (Pool.Peek().activeInHierarchy)
        {
            obj = IsExpandable ? CreateObject() : Pool.Dequeue();
        }
        else
        {
            obj = Pool.Dequeue();
        }

        Pool.Enqueue(obj);
        
        return obj;
    }

    private GameObject CreateObject()
    {
        var obj = Object.Instantiate(Prefab);
        
        Object.DontDestroyOnLoad(obj);

        return obj;
    }
}

public class PoolingSystem : MonoBehaviour
{
    [SerializeField] private List<PrefabPool> _prefabPoolsList;

    private Dictionary<string, ObjectPool> _objectPools;

    public static PoolingSystem Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            CreatePools();   
        }
    }

    private void CreatePools()
    {
        _objectPools = new Dictionary<string, ObjectPool>();

        foreach (PrefabPool prefabPool in _prefabPoolsList.Where(prefabPool => prefabPool.Prefab.GetComponent<IPoolableObject>() != null && !_objectPools.ContainsKey(prefabPool.Tag)))
        {
            _objectPools.Add(prefabPool.Tag, new ObjectPool(prefabPool));
        }
    }


    public GameObject SpawnObject(string tag)
    {
        if (!_objectPools.ContainsKey(tag))
            return null;

        var obj =  _objectPools[tag].GetObject();
        
        obj.SetActive(true);
        obj.GetComponent<IPoolableObject>().OnSpawn();

        return obj;
    }
}
