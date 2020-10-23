using System.Collections.Generic;
using UnityEngine;

public class Production : Institution
{
    private List<Resource> unlockedResources;

    private int incrementalRate;

    private ProductionLevelUpSO levelUpSO;

    public override void Upgrade()
    {
        base.Upgrade();

        UpdateVariables();

        DebugVariables();
    }

    protected override void InitializeMethod()
    {
        levelUpSO = Resources.Load<ProductionLevelUpSO>("ScriptableObjects/ProductionLevelUpSO");

        UpdateVariables();

        DebugVariables();
    }

    protected override void UpdateVariables()
    {
        incrementalRate = levelUpSO.Evaluate(Level);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New production incremental rate = " + incrementalRate);
    }
}
