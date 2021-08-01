using MyBox;
using System.Collections.Generic;
using UnityEngine;

public class CaptureRuleManager : RuleTypeBase
{

    [Separator("Current Rules Data")]

    [ReadOnly]
    public int AmountOfEnemiesToCapture;

    [Separator("Capture Points")]
    [ReadOnly]
    [SerializeField]
    List<CapturePoint> _allcapturePoints = new List<CapturePoint>();

    [ReadOnly] public CapturePoint _featuredCapturePoint;


    private void Awake()
    {
        GetAllCapturePoints();
    }

    protected override void GetRuleData(Rule rule)
    {
        var myRule = (Rule_Capture)rule;

        if (myRule.IsTimed)
        {
            isTimed = true;
            TimeLeft = myRule.RuleTime;
        }

        BuildRuleMessage();
    }

    [ContextMenu("Start Rule")]
    protected override void StartRule()
    {
        if (IsActive == false) return;

        ActivateRandomCapturePoint();

        if (isTimed)
        {
            StopCoroutine(currentRoutine);
            StartCoroutine(currentRoutine);
        }
    }

    private void Update()
    {
        if( IsActive )
        {
            if( _featuredCapturePoint != null)
            {
                CheckClearCondition();
            }
        }
    }

    private void ActivateRandomCapturePoint()
    {
        int randomIndex = Random.Range(0, _allcapturePoints.Count);

        _featuredCapturePoint = _allcapturePoints[randomIndex];

        _featuredCapturePoint.gameObject.SetActive(true);
        _featuredCapturePoint.ActivateCapturePoint();
    }

    protected override void BuildRuleMessage()
    {
        string message = "";
        if( isTimed )
        {
            message = $"Capture o ponto em {TimeLeft.ToString("F0")}";
        }
        else
        {
            message = $"Capture o ponto";
        }
        if (RulesText.instance == null) return;

        RulesText.instance.GetTextToDisplay(message);
    }

    protected override void CheckClearCondition()
    {
        complete = _featuredCapturePoint.isComplete;

        if( complete )
        {
            _featuredCapturePoint.ResetCapturePoint();
            OnRuleCompleted?.Invoke();
            ResetManager();
        }
    }

    void GetAllCapturePoints()
    {
        CapturePoint[] foundPoints = FindObjectsOfType<CapturePoint>();

        foreach (var c in foundPoints)
        {
            if( _allcapturePoints.Contains(c) == false )
            {
                _allcapturePoints.Add(c);
                c.gameObject.SetActive(false);
            }
        }
    }

    protected override void ResetManager()
    {
        TimeLeft = 0;
        _featuredCapturePoint = null;
        isTimed = false;
        IsActive = false;
        complete = false;
    }

    protected override void OnEnemyDeath(Enemy enemy)
    {
       
    }
}
