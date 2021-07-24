using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// individual object pool for using with pooling system
public class Pool
{
    private Stack<PoolableObject> _pooledObjects = new Stack<PoolableObject>();
    private PoolableObject _original;
    private Transform _parent;

    public Pool(PoolableObject original, Transform root, int initialCount)
    {
        _original = original;
        _parent = new GameObject("Pool: " + original.gameObject.name).transform;
        _parent.SetParent(root);
        _parent.transform.localPosition = Vector3.zero;

        Stock(initialCount);
    }

    private void Stock(int count)
    {
        for (int i = 0; i < count; i++)
        {
            PoolableObject newObject = Object.Instantiate(_original);
            newObject.Initialize(this);
            Return(newObject);
        }
    }

    public void Return(PoolableObject toReturn)
    {
        toReturn.PrepareForPool();

        toReturn.InUse = false;
        toReturn.transform.SetParent(_parent);
        toReturn.transform.localPosition = Vector3.zero;
        toReturn.gameObject.SetActive(false);
        _pooledObjects.Push(toReturn);
    }

    public PoolableObject Get()
    {
        if (_pooledObjects.Count < 1)
        {
            Stock(1);
        }

        PoolableObject pooledGameObject = _pooledObjects.Pop();
        pooledGameObject.gameObject.SetActive( true );
        pooledGameObject.LastPullTime = Time.timeSinceLevelLoad;
        pooledGameObject.InUse = true;
        pooledGameObject.PrepareForUse();

        return pooledGameObject;
    }
}
