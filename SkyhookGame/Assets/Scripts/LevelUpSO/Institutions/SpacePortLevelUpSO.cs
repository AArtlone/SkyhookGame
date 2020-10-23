using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "Instituion/Create Space Port Level Up SO")]
public class SpacePortLevelUpSO : LevelUpSOBase
{
    public int minDocksAvailable;
    public int maxDocksAvailable;

    public int minLoadSpeed;
    public int maxLoadSpeed;
    
    public int minUnLoadSpeed;
    public int maxUnLoadSpeed;

    public int EvaluateDocks(int currentLevel)
    {
        return Evaluate(currentLevel, 1, StaticVariables.MaxLevel, minDocksAvailable, maxDocksAvailable);
    }

    public int EvaluateLoadSpeed(int currentLevel)
    {
        return Evaluate(currentLevel, 1, StaticVariables.MaxLevel, minLoadSpeed, maxLoadSpeed);
    }

    public int EvaluateUnLoadSpeed(int currentLevel)
    {
        return Evaluate(currentLevel, 1, StaticVariables.MaxLevel, minUnLoadSpeed, maxUnLoadSpeed);
    }
}
