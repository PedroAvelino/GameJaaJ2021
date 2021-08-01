using System;
using System.Collections;
using UnityEngine;
using MyBox;

public class GameManager : MonoBehaviour
{
    public static Action OnStartRule;

    [SerializeField] float _TimeBetweenRounds =  2f;


    IEnumerator timerRoutine;

    private void OnEnable()
    {
        RulesManager.OnNewRuleGiven += StartRoundTimer;
        RuleTypeBase.OnRuleCompleted += ResetRoutine;
    }

    private void ResetRoutine()
    {
        timerRoutine = null;
        timerRoutine = StartTimerRoutine();
    }

    private void StartRoundTimer( Rule r )
    {
        ResetRoutine();
        StartCoroutine(timerRoutine);
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
        RuleTypeBase.OnRuleCompleted -= ResetRoutine;
    }
}