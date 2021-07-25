using MyBox;
using TMPro;
using UnityEngine;

public class RulesText : MonoBehaviour
{

    public static RulesText instance;
    [AutoProperty(AutoPropertyMode.Parent)] public TextMeshProUGUI _text;


    private void Awake()
    {
        if( instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    private void OnEnable()
    {
        RulesManager.OnNewRuleGiven += GetRuleText;
    }

    private void GetRuleText( Rule rule )
    {
        if( _text == null ) return;

        _text.text = rule.GetRuleText();
    }
    public void GetTextToDisplay( string Text )
    {
        _text.text = Text;
    }

    private void OnDisable()
    {
        RulesManager.OnNewRuleGiven -= GetRuleText;
    }
}
