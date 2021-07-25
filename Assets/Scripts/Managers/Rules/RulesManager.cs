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
        RuleTypeBase.OnRuleCompleted += GetNewRule;
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
        RuleTypeBase.OnRuleCompleted -= GetNewRule;
    }
}
