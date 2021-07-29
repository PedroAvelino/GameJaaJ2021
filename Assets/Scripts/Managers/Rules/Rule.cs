using UnityEngine;
using MyBox;

public abstract class Rule : ScriptableObject
{

    [Separator("Data")]
    [ReadOnly] public RuleType Type = RuleType.Destroy;

    public bool IsTimed;
    [ConditionalField(nameof(IsTimed))] public float RuleTime;


    public int AmountOfEnemiesToSpawn;


    public EnemyType TargetEnemy;

    public abstract string GetRuleText();
    public abstract void AssingType();
}