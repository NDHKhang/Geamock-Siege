using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    void Awake()
    {
        SharedInstance = this;
        SpawnObject();
    }

    void SpawnObject()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = 
                    Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        // Check if tag is in pool
        if (!poolDictionary.ContainsKey(tag)) return null;

        foreach (GameObject obj in poolDictionary[tag])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        return null;
    }
}
