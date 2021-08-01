using UnityEngine;

[CreateAssetMenu(fileName = "SurviveRule_", menuName = "Rules/Survive", order = 3)]
public class Rule_Survive : Rule
{
    public override void AssingType()
    {
        Type = RuleType.Survive;
    }

    public override string GetRuleText()
    {
        return $"Sobreviva por {AmountOfEnemiesToSpawn}";
    }
}