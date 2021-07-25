using System;
using MyBox;
using UnityEngine;


//Deals with Getting new rules and what not
public class RulesManager : MonoBehaviour
{
    [SerializeField] [ReadOnly] [DisplayInspector] Rule _currentRule;

    public static Action<Rule> OnNewRuleGiven;

    private void OnEnable()
    {
        Enemy.OnEnemyDeath += ManageEnemyDeath;
    }

    private void ManageEnemyDeath( Enemy enemy )
    {
        if( _currentRule == null ) return;


        if(_currentRule.TargetEnemy == enemy.Type || _currentRule.TargetEnemy == EnemyType.Any )
        {
            //Deacrease the counter
        }
    }


    [ContextMenu("GetNewRule")]
    public void GetNewRule()
    {
        Rule newRule = RulesLoader.GetRandomRule();

        if( newRule == null ) return;

        _currentRule = newRule;

        OnNewRuleGiven?.Invoke( _currentRule );
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= ManageEnemyDeath;
    }

    private void CheckIfConditionIsMet()
    {
        
    }
}
