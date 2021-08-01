using UnityEngine;

[CreateAssetMenu(fileName = "DestroyRule_", menuName = "Rules/Capture", order = 2)]
public class Rule_Capture : Rule
{
    public override void AssingType()
    {
        Type = RuleType.Capture;
    }

    public override string GetRuleText()
    {
        return $"Capture em {RuleTime}";
    }
}