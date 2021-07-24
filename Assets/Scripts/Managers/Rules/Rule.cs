using UnityEngine;
using MyBox;

public abstract class Rule : ScriptableObject
{

    [Separator("Data")]
    [TextArea(1, 4)]
    public string _ruleText;

    [ReadOnly] public RuleType Type = RuleType.Destroy;
    [SerializeField] bool _isTimed;
    [ConditionalField(nameof(_isTimed))] [SerializeField] float _RuleTime;
    [SerializeField] protected EnemyType _enemyType;

    public abstract string GetRuleText();


    public abstract void AssingType();
}