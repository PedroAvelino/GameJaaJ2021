using MyBox;
using TMPro;
using UnityEngine;

public class RulesText : MonoBehaviour
{
    [AutoProperty(AutoPropertyMode.Parent)] public TextMeshProUGUI _text;

    private void OnEnable()
    {
        RulesManager.OnNewRuleGiven += GetRuleText;
    }

    private void GetRuleText( Rule rule )
    {
        if( _text == null ) return;

        _text.text = rule.GetRuleText();
    }

    private void OnDisable()
    {
        RulesManager.OnNewRuleGiven -= GetRuleText;
    }
}
