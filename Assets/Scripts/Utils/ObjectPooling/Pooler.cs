using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// wrapper class for object pooling functionality
public class Pooler
{
    private static Dictionary<PoolableObject, Pool> _pools = new Dictionary<PoolableObject, Pool>();

    private static Transform _root;

    public static Transform Root
    {
        get
        {
            if (_root == null)
            {
                _root = new GameObject("PoolRoot").transform;
                _root.position = new Vector3(0f, -1000f, -1000f);
            }

            return _root;
        }
    }
    
    public static PoolableObject GetObject(PoolableObject prefab, Vector3 position, Quaternion rotation,
        Transform parent = null, int initialCount = 20)
    {
        // initialize pool if it doesn't exist
        InitializePool(prefab, initialCount);
        
        // get pool object, ready, and return
        PoolableObject pooled = _pools[prefab].Get();

        pooled.transform.position = position;
        pooled.transform.rotation = rotation;
        pooled.transform.SetParent(parent, true);

        return pooled;
    }

    //If a object spawned without the use of the pooler add it to a pool
    public static void AddMissingObject( PoolableObject prefab )
    {   

        //Check if it already has pool to enter
        if (_pools.ContainsKey(prefab))
        {
            prefab.Initialize( _pools[prefab] );
            _pools[prefab].Return( prefab );
        }else { //If it doesn't have a pool create a new one
            InitializePool( prefab );
            prefab.Initialize( _pools[prefab] );
        }

    }

    public static void InitializePool(PoolableObject prefab, int initialCount = 5)
    {
        if (!_pools.ContainsKey(prefab))
        {
            _pools.Add(prefab, new Pool(prefab, Root, initialCount));
            Debug.Log("Pool Initialized: " + prefab.name);
        }
    }

    public static void ClearPool()
    {
        _pools.Clear();
    }
}
