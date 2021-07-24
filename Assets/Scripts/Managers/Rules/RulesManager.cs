using System;
using MyBox;
using UnityEngine;

public class RulesManager : MonoBehaviour
{
    [SerializeField] [ReadOnly] [DisplayInspector] Rule _currentRule;

    public static Action<Rule> OnNewRuleGiven;

    [ContextMenu("GetNewRule")]
    public void GetNewRule()
    {
        Rule newRule = RulesLoader.GetRandomRule();

        if( newRule == null ) return;

        _currentRule = newRule;

        OnNewRuleGiven?.Invoke( _currentRule );
    }

    private void Update()
    {
        if( _currentRule != null)
        {
           CheckIfConditionIsMet();
        }
    }

    private void CheckIfConditionIsMet()
    {
        
    }
}