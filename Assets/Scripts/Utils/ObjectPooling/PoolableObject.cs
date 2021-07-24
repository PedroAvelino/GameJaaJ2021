using System;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{   
    public bool InUse { get; set; }
    public float LastPullTime { get; set; }
    public float CurrentLifeTime => Time.timeSinceLevelLoad - LastPullTime;
    
    private Pool _pool;

    public void Initialize(Pool pool)
    {
        _pool = pool;
    }

    public virtual void PrepareForUse()
    {
        
    }

    public virtual void PrepareForPool()
    {
        
    }

    public void ReturnToPool()
    {
        if( _pool == null )
        {
            Pooler.AddMissingObject( this );
        }
        _pool.Return(this);
    }
}
