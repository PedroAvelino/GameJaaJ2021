using UnityEngine;

public class MainCamera : MonoBehaviour
{

    public static MainCamera instance;

    [SerializeField] StressReceiver _receiver;

    [Range(0f, 1f)]
    [SerializeField] float _stressAmount = .7f;

    private void Awake()
    {
        if( instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    [ContextMenu("ReceiveStress")]
    public void  ReceiveStress()
    {
        _receiver.InduceStress(_stressAmount);
    }
}
