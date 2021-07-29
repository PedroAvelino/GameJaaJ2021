using MyBox;
using System;
using System.Collections;
using UnityEngine;

//This class is the base for expecific rule managers
public abstract class RuleTypeBase : MonoBehaviour
{

    [Separator("Base Data")]
    [SerializeField] 
    RuleType TargetRule;

    [ReadOnly]
    public float TimeLeft = -1;

    [SerializeField]
    [ReadOnly]
    protected bool isTimed;

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
        //Enemy.OnEnemyCaptured += OnEnemyDeath;
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
        //Enemy.OnEnemyCaptured -= OnEnemyDeath;
        GameManager.OnStartRule -= StartRule;
    }

    protected virtual void RuleFailed()
    {
        if (IsActive == false) return;
        OnRuleFailed?.Invoke(  );

        print("Failed");
        ResetManager();
    }

    protected virtual void RuleCompleted()
    {

        if (IsActive == false) return;
        complete = true;
        OnRuleCompleted?.Invoke(  );
    }

    protected virtual IEnumerator StartTimerRoutine( )
    {
        if (IsActive == false) yield return null;

        while (TimeLeft > 0 && complete == false)
        {
            TimeLeft -= Time.deltaTime;
            BuildRuleMessage();
            yield return null;
        }

        if( TimeLeft <= 0f )
        {
            RuleFailed();
        }
    }


    protected abstract void BuildRuleMessage();
    protected abstract void StartRule();
    protected abstract void GetRuleData( Rule rule );
    protected abstract void OnEnemyDeath( Enemy enemy );
    protected abstract void ResetManager();
    protected abstract void CheckClearCondition();

}
