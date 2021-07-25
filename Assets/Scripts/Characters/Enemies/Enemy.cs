using System;
using UnityEngine;

public abstract class Enemy : PoolableObject
{
    public EnemyType Type;
    public static Action<Enemy> OnEnemyDeath;

    public virtual void Death()
    {
        OnEnemyDeath?.Invoke( this );
        ReturnToPool();
    }
}
