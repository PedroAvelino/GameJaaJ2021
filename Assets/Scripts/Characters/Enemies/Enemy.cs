using System;
using UnityEngine;
using MyBox;

public abstract class Enemy : PoolableObject
{

    [SerializeField] float _speed = 2f;
    public EnemyType Type;
    public static Action<Enemy> OnEnemyDeath;
    public static Action<Enemy> OnEnemyCaptured;

    public Transform destination;

    Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if( destination != null )
        {
            MoveToDestination();
        }
    }

    void MoveToDestination()
    {
        
        float distance = Vector2.Distance( transform.position, destination.transform.position );
        if( distance < 0.1f )
        {
            ReturnToPool();
        }
        else
        {
            Vector2 dir = (destination.transform.position - transform.position).normalized;
            _rb.velocity = dir * _speed;

        }
    }
    public virtual void Death()
    {
        //Tava sem tempo aqui lol
        AudioManager.instance.Play("enemy_death");
        MainCamera.instance.ReceiveStress();

        OnEnemyDeath?.Invoke( this );
        ReturnToPool();
    }

    public override void PrepareForPool()
    {
        destination = null;
    }

    public override void PrepareForUse()
    {
        destination = null;
    }
}
