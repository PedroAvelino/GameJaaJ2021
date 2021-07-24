using UnityEngine;

public class RulesManager : MonoBehaviour
{

    const string RULES_PATH = "Rules";
    [SerializeField] Rule _currentRule;

    void LoadRules()
    {

    }
    Rule GetRandomRule()
    {
        return new Rule_Destroy();
    }
}
