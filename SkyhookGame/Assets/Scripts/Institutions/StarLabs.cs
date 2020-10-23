using System.Collections.Generic;
using UnityEngine;

public class StarLabs : Institution
{
    [SerializeField] private Vector2Int tasksCapacityRange = default;

    private List<Study> availableStudies;
    private List<Study> studiesInProgress;

    private int tasksCapacity;

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
        tasksCapacity = LevelModule.Evaluate(tasksCapacityRange);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New RD tasks capacity = " + tasksCapacity);
    }
}
