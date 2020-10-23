using UnityEngine;

public class Factory : Institution
{
    private int storageCapacity;
    private int tasksCapacity;

    private FactoryLevelUpSO levelUpSO;

    public override void Upgrade()
    {
        base.Upgrade();

        storageCapacity = levelUpSO.EvaluateStorageCapacity(Level);
        tasksCapacity = levelUpSO.EvaluateTasksCapacity(Level);

        DebugVariables();
    }

    protected override void InitializeMethod()
    {
        levelUpSO = Resources.Load<FactoryLevelUpSO>("ScriptableObjects/FactoryLevelUpSO");

        UpdateVariables();

        DebugVariables();
    }

    protected override void UpdateVariables()
    {
        storageCapacity = levelUpSO.EvaluateStorageCapacity(Level);
        tasksCapacity = levelUpSO.EvaluateTasksCapacity(Level);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New Storage Capacity number = " + storageCapacity);
        Debug.Log("New Tasks Capacity number = " + tasksCapacity);
    }
}
