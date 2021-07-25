using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnStartRule;


    [ContextMenu("Start Rule")]
    public void CallStartRule()
    {
        OnStartRule?.Invoke();
    }
}