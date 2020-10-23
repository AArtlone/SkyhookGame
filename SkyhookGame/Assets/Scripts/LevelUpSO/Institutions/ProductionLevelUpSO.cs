using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "Instituion/Create Production Level Up SO")]
public class ProductionLevelUpSO : LevelUpSOBase
{
    public int minIncrementalRate;
    public int maxIncrementalRate;

    public int Evaluate(int currentLevel)
    {
        return Evaluate(currentLevel, 1, StaticVariables.MaxLevel, minIncrementalRate, maxIncrementalRate);
    }
}
