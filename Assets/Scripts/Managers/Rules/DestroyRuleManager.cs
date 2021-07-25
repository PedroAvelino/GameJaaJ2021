using MyBox;
using UnityEngine;

//Deals with rules of the type destroy
public class DestroyRuleManager : RuleTypeBase
{

    [Separator("Current Rules Data")]
    
    [ReadOnly] 
    public float TimeLeft = -1;
    
    [SerializeField] [ReadOnly] 
    bool _isTimed;
    
    [ReadOnly] 
    public int AmountOfEnemiesToDestroy;
    
    [ReadOnly] 
    public EnemyType TargetEnemy;

    protected override void GetRuleData(Rule rule)
    {
        var myRule = (Rule_Destroy)rule;

        if( myRule.IsTimed )
        {
            _isTimed = true;
            TimeLeft = myRule.RuleTime;
        }

        TargetEnemy = myRule.TargetEnemy;
        AmountOfEnemiesToDestroy = myRule.AmountOfEnemiesToDestroy;
    }

    protected override void StartRule()
    {
        if (IsActive == false) return;
        
        if(_isTimed)
        {
            StartCoroutine( StartTimer( TimeLeft ));
        }
    }

    protected override void OnEnemyDeath(Enemy enemy)
    {
        if( IsActive == false || enemy == null ) return;

        if (TargetEnemy == EnemyType.Any || TargetEnemy == enemy.Type)
        {
            if( AmountOfEnemiesToDestroy > 0 )
            {
                DecreaseOneEnemy();
                CheckClearCondition();
            }
            else
            {
                CheckClearCondition();
            }

        }
    }

    [ContextMenu("Decrease Enemy Count")]
    void DecreaseOneEnemy()
    {
        AmountOfEnemiesToDestroy--;
        BuildMessage();
    }

    void BuildMessage()
    {
        string message = "";

        if( _isTimed )
        {
            message = $"Destrua {AmountOfEnemiesToDestroy} de {TargetEnemy} em {TimeLeft}";
        }
        message = $"Destrua {AmountOfEnemiesToDestroy} de {TargetEnemy}";

        if(RulesText.instance == null) return;

        RulesText.instance.GetTextToDisplay( message );
    }

    private void CheckClearCondition()
    {
        bool allEnemiesKilled = (AmountOfEnemiesToDestroy <= 0);


        if( allEnemiesKilled )
        {

            RuleCompleted();
        }
    }

    protected override void ResetManager()
    {
        TimeLeft = 0;
        _isTimed = false;
        AmountOfEnemiesToDestroy = 0;
        TargetEnemy = EnemyType.Any;
        IsActive = false;
        complete = false;
    }
}
