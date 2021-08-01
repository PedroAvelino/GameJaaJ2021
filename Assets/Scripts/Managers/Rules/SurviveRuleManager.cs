using System.Collections;
using UnityEngine;

public class SurviveRuleManager : RuleTypeBase
{
    protected override void BuildRuleMessage()
    {
        string message = "";
        message = $"Sobreviva por {TimeLeft.ToString("F0")}";
        if (RulesText.instance == null) return;

        RulesText.instance.GetTextToDisplay(message);
    }

    protected override void CheckClearCondition()
    {
        if( TimeLeft <=  0f  )
        {
            RuleCompleted();
        }
    }

    protected override void GetRuleData(Rule rule)
    {
        var myRule = (Rule_Survive)rule;

        if (myRule.IsTimed)
        {
            isTimed = true;
            TimeLeft = myRule.RuleTime;
        }

        BuildRuleMessage();
    }

    protected override void OnEnemyDeath(Enemy enemy)
    {
        
    }

    protected override void ResetManager()
    {
       
    }

    protected override void StartRule()
    {
        if (IsActive == false) return;

        if (isTimed)
        {
            StopCoroutine(currentRoutine);
            StartCoroutine(currentRoutine);
        }
    }
    protected override IEnumerator StartTimerRoutine()
    {
        if (IsActive == false) yield return null;

        bool onLoop = true;

        while (onLoop)
        {

            if (TimeLeft <= 0 || complete)
            {
                onLoop = false;
                yield return null;
            }

            TimeLeft -= Time.deltaTime;
            BuildRuleMessage();
            yield return null;
        }

        if (TimeLeft <= 0f)
        {
            RuleCompleted();
        }
    }
}
