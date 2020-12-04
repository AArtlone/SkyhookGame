using UnityEngine;

public class StarLabs : Institution
{
    [SerializeField] private Vector2Int tasksCapacityRange = default;

    private int tasksCapacity;

    protected override void InitializeMethod()
    {
        UpdateVariables();
        DebugVariables();
    }

    protected override void UpdateVariables()
    {
        base.UpdateVariables();

        tasksCapacity = LevelModule.Evaluate(tasksCapacityRange);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New RD tasks capacity = " + tasksCapacity);
    }
}
