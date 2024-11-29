using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool
{
    public string nameId;
    public int poolSize;
    public GameObject poolObject;
}

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance => instance;
    private static ObjectPooling instance;
    public List<ObjectPool> pools = new List<ObjectPool>();
    private Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, Transform> poolParents = new Dictionary<string, Transform>();

    private void Awake() 
    {
        if(instance == null)
            instance = this;
    }

    private void Start() 
    {
        foreach (ObjectPool pool in pools)
        {
            InitializePool(pool);
        }
    }
    private void InitializePool(ObjectPool pool)
    {
        GameObject parent = new GameObject(pool.nameId + "_Pool");
        parent.transform.SetParent(transform);
        poolParents[pool.nameId] = parent.transform;
        
        Queue<GameObject> objectPool = new Queue<GameObject>();
        for (int i = 0; i < pool.poolSize; i++)
        {
            GameObject obj = CreatePoolObject(pool, parent.transform);
            objectPool.Enqueue(obj);
        }
        poolDict.Add(pool.nameId, objectPool);
    }
    private GameObject CreatePoolObject(ObjectPool pool, Transform parent)
    {
        GameObject obj = Instantiate(pool.poolObject, parent);
        obj.name = pool.nameId;
        
        if (obj.TryGetComponent<ISpawnObject>(out var _module))
            ResetObject(_module);
        
        obj.SetActive(false);
        return obj;
    }
    public GameObject GetFromPool(string nameId,Vector3 position,Quaternion rotation)
    {
        if(!poolDict.TryGetValue(nameId, out var _value))
        {
            Debug.LogError("Pool doesn't exist");
            return null;
        }
        if (_value.Count == 0)
        {
            ExpandPool(nameId);
        }

        GameObject spawnObj = poolDict[nameId].Dequeue();
        spawnObj.transform.SetParent(null);
        spawnObj.SetActive(true);
        spawnObj.transform.position = position;
        spawnObj.transform.rotation = rotation;
        
        if (spawnObj.TryGetComponent<ISpawnObject>(out var _module))
            _module.PlayModule();
        
        return spawnObj;
    }
    
    public void ReturnToPool(GameObject obj)
    {
        if (!poolDict.ContainsKey(obj.name)) return;
        
        if (obj.TryGetComponent<ISpawnObject>(out var _module)) 
            ResetObject(_module);
        
        obj.SetActive(false);
        obj.transform.SetParent(poolParents[obj.name]);
        poolDict[obj.name].Enqueue(obj);
    }

    private void ExpandPool(string nameId)
    {
        ObjectPool pool = pools.Find(p => p.nameId == nameId);
        if (pool == null) return;

        GameObject obj = CreatePoolObject(pool, poolParents[nameId]);
        poolDict[nameId].Enqueue(obj);
    }

    private void ResetObject(ISpawnObject _module)
    {
        _module.ResetModule();
    }
}
