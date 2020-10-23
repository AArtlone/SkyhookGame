using System.Collections.Generic;
using UnityEngine;

public class Production : Institution
{
    [SerializeField] private Vector2Int incremetalRateRange = default;
    
    private List<Resource> unlockedResources;

    private int incrementalRate;

    public override void Upgrade()
    {
        base.Upgrade();

        UpdateVariables();

        DebugVariables();
    }

    protected override void InitializeMethod()
    {
        UpdateVariables();

        DebugVariables();
    }

    protected override void UpdateVariables()
    {
        incrementalRate = LevelModule.Evaluate(incremetalRateRange);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New production incremental rate = " + incrementalRate);
    }
}
