using System.Collections.Generic;
using UnityEngine;

public static class RulesLoader
{
    const string RULES_PATH = "Rules";
    private static List<Rule> Rules;
    private static bool _isInit;

    private static void Init()
    {
        Rules = new List<Rule>();
        LoadRules();
        _isInit = true;
    }

    private static void LoadRules()
    {
        Rule[] foundRules = Resources.LoadAll<Rule>(RULES_PATH);

        foreach (Rule r in foundRules)
        {
            if( r == null ) continue;

            r.AssingType();

            Rules.Add( r );
        }
    }

    public static Rule GetRandomRule()
    {
        if( _isInit == false)
        {
            Init();
        }
        int totalRules = Rules.Count;
        int randomIndex = Random.Range(0 , totalRules );
        Rule foundRule = Rules[randomIndex];

        if(  foundRule == null )
        {
            return GetRandomRule();
        }
        return foundRule;
    }
}
