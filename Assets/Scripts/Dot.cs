using System.Collections;
using UnityEditor;
using UnityEngine;
using MyBox;
using System;

[RequireComponent(typeof(Player))]
public class Dot : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float _playerSpeed = 7f;

    [SerializeField] float _dashSpeed = 13f;
    [SerializeField] float _dashTime = .3f;
    [SerializeField] float _dashCooldown = 0.25f;
    [SerializeField] bool _isDashing;
    [SerializeField] bool _canDash = true;


    float _currentPlayerSpeed;
    [Separator("Components")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] SpriteRenderer _sr;

    [AutoProperty] [SerializeField]
    AudioSource _dashSound;

    [SerializeField] CanvasController canvasController;

    public static Action OnDeath;

#if UNITY_EDITOR
    private void OnValidate()
    {
        Init();

        EditorUtility.SetDirty(this);
    }
#endif

    private void Init()
    {
        if (!_rb)
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        if (!_sr)
        {
            _sr = GetComponent<SpriteRenderer>();
        }
    }

    private void Awake()
    {
        _currentPlayerSpeed = _playerSpeed;
    }

    public void Move(Vector2 moveDirection)
    {
        _rb.velocity = moveDirection * _currentPlayerSpeed;
    }

    public void TryDash( Vector2 moveDirection )
    {
        if( _isDashing || _canDash == false ) return;
        if (moveDirection == Vector2.zero) return;

        StartCoroutine( Dash() );
    }

    private IEnumerator Dash()
    {
        _isDashing = true;
        _canDash = false;

        Color originalColor = _sr.color;
        _sr.color = Color.blue;
        
        _currentPlayerSpeed = _dashSpeed;

        _dashSound.Play();
        yield return new WaitForSeconds( _dashTime );

        _isDashing = false;
        _sr.color = originalColor;

        _currentPlayerSpeed = _playerSpeed;

        StartCoroutine( DashCooldown() );
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds( _dashCooldown );

        _canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if( enemy != null && _isDashing )
        {
            enemy.Death();
        }
        else
        {
            Death();
        }
    }

    [ContextMenu("Death")]
    private void Death()
    {
        OnDeath?.Invoke();
        AudioManager.instance.Play("playerDeath");
        AudioManager.instance.Stop("theme");
        AudioManager.instance.Play("gameover");
    }

    private void Update()
    {
        if (Input.GetKeyDown( KeyCode.J ))
        {
            Death();
        }
    }
}