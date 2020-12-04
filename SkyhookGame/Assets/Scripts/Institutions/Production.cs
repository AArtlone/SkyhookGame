﻿using UnityEngine;

public class Production : Institution
{
    [SerializeField] private Vector2Int incremetalRateRange = default;

    private int incrementalRate;
    protected override void InitializeMethod()
    {
        UpdateVariables();
        DebugVariables();
    }

    protected override void UpdateVariables()
    {
        base.UpdateVariables();

        incrementalRate = LevelModule.Evaluate(incremetalRateRange);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New production incremental rate = " + incrementalRate);
    }
}
