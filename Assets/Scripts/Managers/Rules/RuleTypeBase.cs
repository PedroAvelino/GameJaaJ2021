using MyBox;
using UnityEngine;

//This class is the base for expecific rule managers
public abstract class RuleTypeBase : MonoBehaviour
{
    [SerializeField] RuleType TargetRule;
    [ReadOnly] public bool IsActive;

    private void OnEnable()
    {
        RulesManager.OnNewRuleGiven += CheckIfIsTargetRule;
        Enemy.OnEnemyDeath += OnEnemyDeath;
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
    }

    protected abstract void GetRuleData( Rule rule );
    protected abstract void OnEnemyDeath( Enemy enemy );
}
