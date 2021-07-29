using UnityEngine;

[CreateAssetMenu(fileName = "DestroyRule_", menuName = "Rules/Destroy", order = 1)]
public class Rule_Destroy : Rule
{

    public override void AssingType()
    {
        Type = RuleType.Destroy;
    }

    public override string GetRuleText()
    {
        return $"Destrua {AmountOfEnemiesToSpawn} de {TargetEnemy}";
    }
}
