using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "Instituion/Create Factory Level Up SO")]
public class FactoryLevelUpSO : LevelUpSOBase
{
    public int minTasksCapacity;
    public int maxTasksCapacity;

    public int minStorageCapacity;
    public int maxStorageCapacity;

    public int EvaluateTasksCapacity(int currentLevel)
    {
        return Evaluate(currentLevel, 1, StaticVariables.MaxLevel, minTasksCapacity, maxTasksCapacity);
    }

    public int EvaluateStorageCapacity(int currentLevel)
    {
        return Evaluate(currentLevel, 1, StaticVariables.MaxLevel, minStorageCapacity, maxStorageCapacity);
    }
}
