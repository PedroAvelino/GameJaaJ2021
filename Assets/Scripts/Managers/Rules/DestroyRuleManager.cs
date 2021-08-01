using MyBox;
using UnityEngine;

//Deals with rules of the type destroy
public class DestroyRuleManager : RuleTypeBase
{

    [Separator("Current Rules Data")]
    
    [ReadOnly] 
    public int AmountOfEnemiesToDestroy;
    
    [ReadOnly] 
    public EnemyType TargetEnemy;

    protected override void GetRuleData(Rule rule)
    {
        var myRule = (Rule_Destroy)rule;//We are assuming this is a destroy rule

        if( myRule.IsTimed )
        {
            isTimed = true;
            TimeLeft = myRule.RuleTime;
        }

        TargetEnemy = myRule.TargetEnemy;
        AmountOfEnemiesToDestroy = myRule.AmountOfEnemiesToSpawn;
        BuildRuleMessage();
    }

    [ContextMenu("Start Rule")]
    protected override void StartRule()
    {
        if (IsActive == false) return;

        if (isTimed)
        {
            StopCoroutine( currentRoutine );
            StartCoroutine( currentRoutine );
        }
    }

    protected override void OnEnemyDeath(Enemy enemy)
    {
        if( IsActive == false || enemy == null ) return;

        if (TargetEnemy == EnemyType.Any || TargetEnemy == enemy.Type)
        {
            if( AmountOfEnemiesToDestroy > 0 )
            {
                DecreaseOneEnemy();
                CheckClearCondition();
            }
            else
            {
                CheckClearCondition();
            }

        }
    }

    [ContextMenu("Decrease Enemy Count")]
    void DecreaseOneEnemy()
    {
        AmountOfEnemiesToDestroy--;
        BuildRuleMessage();
    }

    protected override void BuildRuleMessage()
    {
        string message = "";

        if ( isTimed )
        {
            message = $"Destrua {AmountOfEnemiesToDestroy} de {TargetEnemy} em {TimeLeft.ToString("F0")}";
        }
        else
        {
            message = $"Destrua {AmountOfEnemiesToDestroy} de {TargetEnemy}";
        }

        if(RulesText.instance == null) return;

        RulesText.instance.GetTextToDisplay( message );
    }

    protected  override void CheckClearCondition()
    {
        bool allEnemiesKilled = (AmountOfEnemiesToDestroy <= 0);


        if( allEnemiesKilled )
        {

            RuleCompleted();
        }
    }

    protected override void ResetManager()
    {
        TimeLeft = 0;
        isTimed = false;
        AmountOfEnemiesToDestroy = 0;
        TargetEnemy = EnemyType.Any;
        IsActive = false;
        complete = false;   
    }
}
