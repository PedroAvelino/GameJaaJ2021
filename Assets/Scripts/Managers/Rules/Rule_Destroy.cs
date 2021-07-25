using UnityEngine;

[CreateAssetMenu(fileName = "DestroyRule_", menuName = "Rules/Destroy", order = 1)]
public class Rule_Destroy : Rule
{
    public int AmountOfEnemiesToDestroy;

    public override void AssingType()
    {
        Type = RuleType.Destroy;
    }

    public override string GetRuleText()
    {
        return $"Destrua {AmountOfEnemiesToDestroy} de {TargetEnemy}";
    }
}
