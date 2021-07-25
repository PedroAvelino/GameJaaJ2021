using MyBox;
using UnityEngine;

public class DestroyRuleManager : RuleTypeBase
{

    [Separator("Current Rules Data")]
    [ReadOnly] public float TimeLeft;
    [ReadOnly] public int AmountOfEnemiesToDestroy;
    [ReadOnly] public EnemyType TargetEnemy;

    protected override void GetRuleData(Rule rule)
    {
        var myRule = (Rule_Destroy)rule;

        if( myRule.IsTimed )
        {
            TimeLeft = myRule.RuleTime;
        }

        TargetEnemy = myRule.TargetEnemy;
        AmountOfEnemiesToDestroy = myRule.AmountOfEnemiesToDestroy;
    }

    protected override void OnEnemyDeath(Enemy enemy)
    {
        if( IsActive == false || enemy == null ) return;

        if (TargetEnemy == EnemyType.Any || TargetEnemy == enemy.Type)
        {
            
            if( AmountOfEnemiesToDestroy > 0 )
            {
                DecreaseOneEnemy();
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
        BuildMessage();
    }

    void BuildMessage()
    {
        string message = $"Destrua {AmountOfEnemiesToDestroy} de {TargetEnemy}";

        if(RulesText.instance == null) return;

        RulesText.instance.GetTextToDisplay( message );
    }

    private void CheckClearCondition()
    {
        
    }
}
