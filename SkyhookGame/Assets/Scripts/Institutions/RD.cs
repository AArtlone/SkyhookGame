using System.Collections.Generic;
using UnityEngine;

public class RD : Institution
{
    private List<Study> availableStudies;
    private List<Study> studiesInProgress;

    private int tasksCapacity;

    private RDLevelUpSO levelUpSO;

    public override void Upgrade()
    {
        base.Upgrade();

        UpdateVariables();

        DebugVariables();
    }

    protected override void InitializeMethod()
    {
        levelUpSO = Resources.Load<RDLevelUpSO>("ScriptableObjects/RDLevelUpSO");

        UpdateVariables();

        DebugVariables();
    }

    protected override void UpdateVariables()
    {
        tasksCapacity = levelUpSO.Evaluate(Level);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New RD tasks capacity = " + tasksCapacity);
    }
}
