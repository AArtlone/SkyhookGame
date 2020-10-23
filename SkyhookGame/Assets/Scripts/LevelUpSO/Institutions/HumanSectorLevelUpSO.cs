using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "Instituion/Create Human Sector Level Up SO")]
public class HumanSectorLevelUpSO : LevelUpSOBase
{
    public int minHumans;
    public int maxHumans;

    public int Evaluate(int currentLevel)
    {
        return Evaluate(currentLevel, 1, StaticVariables.MaxLevel, minHumans, maxHumans);
    }
}
