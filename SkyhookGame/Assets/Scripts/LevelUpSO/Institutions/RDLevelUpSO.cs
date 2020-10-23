using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "Instituion/Create RD Level Up SO")]
public class RDLevelUpSO : LevelUpSOBase
{
    public int minTasksCapacity;
    public int maxTasksCapacity;

    public int Evaluate(int currentLevel)
    {
        return Evaluate(currentLevel, 1, StaticVariables.MaxLevel, minTasksCapacity, maxTasksCapacity);
    }
}
