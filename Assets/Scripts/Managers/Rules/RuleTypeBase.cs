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

    protected IEnumerator currentRoutine;

    private void OnEnable()
    {
        RulesManager.OnNewRuleGiven += CheckIfIsTargetRule;
        Enemy.OnEnemyDeath += OnEnemyDeath;
        Dot.OnDeath += StopEverything;
        GameManager.OnStartRule += StartRule;
    }

    private void StopEverything()
    {
        if( currentRoutine != null )
        {
            StopCoroutine(currentRoutine);
        }
        RulesText.instance.GetTextToDisplay("Lmao");
    }

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        currentRoutine = StartTimerRoutine();
    }

    private void CheckIfIsTargetRule( Rule rule )
    {
        ResetManager();

        if ( rule == null ) return;

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
        Dot.OnDeath -= StopEverything;
    }

    protected virtual void RuleFailed()
    {
        if (IsActive == false) return;
        OnRuleFailed?.Invoke(  );

        print("Failed");
        StopEverything();
        ResetManager();
    }

    protected virtual void RuleCompleted()
    {

        if (IsActive == false) return;

        AudioManager.instance.Play("winRound");
        
        StopCoroutine(currentRoutine);
        currentRoutine = null;
        currentRoutine = StartTimerRoutine();

        OnRuleCompleted?.Invoke(  );
    }

    //Eu odeio coroutines com todas as minhas forças puta que pariu
    protected virtual IEnumerator StartTimerRoutine( )
    {
        if (IsActive == false) yield return null;

        bool onLoop = true;

        while ( onLoop )
        {

            if (TimeLeft <= 0 || complete )
            {
                onLoop = false;
                yield return null;
            }

            TimeLeft -= Time.deltaTime;
            BuildRuleMessage();
            yield return null;
        }

        if ( TimeLeft <= 0f )
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
