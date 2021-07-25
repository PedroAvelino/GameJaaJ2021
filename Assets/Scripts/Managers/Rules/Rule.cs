using UnityEngine;
using MyBox;

public abstract class Rule : ScriptableObject
{

    [Separator("Data")]
    [TextArea(1, 4)]
    public string _ruleText;

    [ReadOnly] public RuleType Type = RuleType.Destroy;

    public bool IsTimed;
    [ConditionalField(nameof(IsTimed))] public float RuleTime;


    public EnemyType TargetEnemy;

    public abstract string GetRuleText();
    public abstract void AssingType();
}