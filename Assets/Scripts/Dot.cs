using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Dot : MonoBehaviour
{
    [SerializeField] float _playerSpeed = 7f;

    [SerializeField] Rigidbody2D _rb;

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
    }

    public void Move(Vector2 moveDirection)
    {
        _rb.velocity = moveDirection * _playerSpeed;
    }
}