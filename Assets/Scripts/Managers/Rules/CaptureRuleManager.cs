using MyBox;
using UnityEngine;

public class CaptureRuleManager : RuleTypeBase
{

    [Separator("Current Rules Data")]

    [ReadOnly]
    public int AmountOfEnemiesToCapture;

    [ReadOnly]
    public EnemyType TargetEnemy;

    protected override void GetRuleData(Rule rule)
    {
        var myRule = (Rule_Destroy)rule;

        if (myRule.IsTimed)
        {
            isTimed = true;
            TimeLeft = myRule.RuleTime;
        }

        TargetEnemy = myRule.TargetEnemy;
        AmountOfEnemiesToCapture = myRule.AmountOfEnemiesToSpawn;
        BuildRuleMessage();
    }

    [ContextMenu("Start Rule")]
    protected override void StartRule()
    {
        if (IsActive == false) return;

        if (isTimed)
        {
            StartCoroutine(StartTimerRoutine());
        }
    }

    protected override void OnEnemyDeath(Enemy enemy)
    {
        if (IsActive == false || enemy == null) return;

        if (TargetEnemy == EnemyType.Any || TargetEnemy == enemy.Type)
        {
            if (AmountOfEnemiesToCapture > 0)
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
        AmountOfEnemiesToCapture--;
        BuildRuleMessage();
    }

    protected override void BuildRuleMessage()
    {
        string message = "";

        if (isTimed)
        {
            message = $"Capture {AmountOfEnemiesToCapture} de {TargetEnemy} em {TimeLeft.ToString("F0")}";
        }
        else
        {
            message = $"Capture {AmountOfEnemiesToCapture} de {TargetEnemy}";
        }

        if (RulesText.instance == null) return;

        RulesText.instance.GetTextToDisplay(message);
    }

    protected override void CheckClearCondition()
    {
        bool allEnemiesCaptured = (AmountOfEnemiesToCapture <= 0);


        if (allEnemiesCaptured)
        {

            RuleCompleted();
        }
    }

    protected override void ResetManager()
    {
        TimeLeft = 0;
        isTimed = false;
        AmountOfEnemiesToCapture = 0;
        TargetEnemy = EnemyType.Any;
        IsActive = false;
        complete = false;
    }
}
