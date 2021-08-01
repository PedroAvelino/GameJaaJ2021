using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Dot _dot;

#if UNITY_EDITOR
    private void OnValidate()
    {
        Init();

        EditorUtility.SetDirty(this);
    }
#endif

    private void Init()
    {
        if (!_dot)
        {
            _dot = GetComponent<Dot>();
        }
    }

    private void Update()
    {
        if( Input.GetKeyDown(KeyCode.Z))
        {
            _dot.TryDash(GetMoveVector());
        }
    }

    private void FixedUpdate()
    {
        _dot.Move( GetMoveVector() );
    }

    Vector2 GetMoveVector()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2( h , v ).normalized;
        return moveDirection;
    }
}
