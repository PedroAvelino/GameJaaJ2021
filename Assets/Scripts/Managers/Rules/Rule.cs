using UnityEngine;

public abstract class Rule : ScriptableObject
{
    [TextArea(1, 4)]
    public string _ruleText;
}
