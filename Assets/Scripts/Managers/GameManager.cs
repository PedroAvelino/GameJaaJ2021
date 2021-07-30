using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnStartRule;

    [SerializeField] float _TimeBetweenRounds =  2f;


    IEnumerator currentRoutine;

    private void OnEnable()
    {
        RulesManager.OnNewRuleGiven += StartRoundTimer;
    }

    private void Awake()
    {
        currentRoutine = StartTimerRoutine();
    }

    private void StartRoundTimer( Rule r )
    {
        StopCoroutine( currentRoutine );
        StartCoroutine( currentRoutine );
    }

    IEnumerator StartTimerRoutine()
    {
        yield return new WaitForSeconds(_TimeBetweenRounds);

        CallStartRule();
    }

    [ContextMenu("Start Rule")]
    public void CallStartRule()
    {
        OnStartRule?.Invoke();
    }

    private void OnDisable()
    {
        RulesManager.OnNewRuleGiven -= StartRoundTimer;
    }
}