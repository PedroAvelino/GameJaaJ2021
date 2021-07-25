using MyBox;
using System;
using System.Collections;
using UnityEngine;

//This class is the base for expecific rule managers
public abstract class RuleTypeBase : MonoBehaviour
{
    [SerializeField] 
    RuleType TargetRule;
    
    [ReadOnly] 
    public bool IsActive;

    public static Action OnRuleFailed;
    public static Action OnRuleCompleted;

    [SerializeField] [ReadOnly] 
    protected bool complete;

    private void OnEnable()
    {
        RulesManager.OnNewRuleGiven += CheckIfIsTargetRule;
        Enemy.OnEnemyDeath += OnEnemyDeath;
        GameManager.OnStartRule += StartRule;
    }


    private void CheckIfIsTargetRule( Rule rule )
    {
        if( rule == null ) return;

        if( rule.Type == TargetRule )
        {
            GetRuleData( rule );
            IsActive = true;
        }
    }

    private void OnDisable()
    {
        RulesManager.OnNewRuleGiven -= CheckIfIsTargetRule;
        Enemy.OnEnemyDeath -= OnEnemyDeath;
        GameManager.OnStartRule -= StartRule;
    }

    protected virtual void RuleFailed()
    {
        if (IsActive == false) return;
        OnRuleFailed?.Invoke(  );

        ResetManager();
    }

    protected virtual void RuleCompleted()
    {

        if (IsActive == false) return;
        complete = true;
        OnRuleCompleted?.Invoke(  );
    }

    protected virtual IEnumerator StartTimer( float time )
    {
        if (IsActive == false) yield return null;

        while (time > 0 )
        {
            if( complete )
            {
                yield break;
            }

            time -= Time.deltaTime;

            yield return null;
        }
    }

    protected abstract void StartRule();
    protected abstract void GetRuleData( Rule rule );
    protected abstract void OnEnemyDeath( Enemy enemy );
    protected abstract void ResetManager();

}
