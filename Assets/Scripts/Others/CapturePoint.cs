using UnityEngine;
using MyBox;

public class CapturePoint : MonoBehaviour
{


    [SerializeField]
    Color _startColor = Color.white;

    [SerializeField]
    Color _endColor = Color.green;

    [SerializeField]
    float _fillMaximum = 100f;

    [SerializeField]
    float _fillSpeed = 1f;

    [ReadOnly]
    public bool IsActive = false;

    [ReadOnly]
    public bool isComplete;

    [ReadOnly]
    public float currentFill = 0f;

    [SerializeField] [ReadOnly]
    bool isFilling;


    [AutoProperty] [SerializeField]
    SpriteRenderer _sr;

    private void Update()
    {
        if (IsActive == false) return;

        if( isFilling )
        {
            if( currentFill < _fillMaximum )
            {
                currentFill += Time.deltaTime * _fillSpeed;

                float blendPosition = (currentFill / 100f);
                Color lerpColor = Color.Lerp(_startColor, _endColor, blendPosition);

                _sr.color = lerpColor;

            }
            else
            {
                isComplete = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Dot p = collision.GetComponent<Dot>();
        if( p != null )
        {
            isFilling = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Dot p = collision.GetComponent<Dot>();
        if (p != null)
        {
            isFilling = false;
        }
    }

    public void ResetCapturePoint()
    {
        currentFill = 0f;
        isComplete = false;
        isFilling = false;
        IsActive = false;
        _sr.color = _startColor;
        gameObject.SetActive(false);
    }

    internal void ActivateCapturePoint()
    {
        IsActive = true;
    }
}
