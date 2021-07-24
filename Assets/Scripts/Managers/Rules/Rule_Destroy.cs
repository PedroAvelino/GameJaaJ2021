using MyBox;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyRule_", menuName = "ScriptableObjects/Rules/Destroy", order = 1)]
public class Rule_Destroy : Rule
{
    [SerializeField] int _AmountOfEnemiesToDestroy;

    public override void AssingType()
    {
        Type = RuleType.Destroy;
    }

    public override string GetRuleText()
    {
        return $"Destrua {_AmountOfEnemiesToDestroy} de {_enemyType}";
    }
}
