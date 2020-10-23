using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "Create Settlement Level Up SO")]
public class SettlementLevelUpSO : LevelUpSOBase
{
    public int minXPValue;
    public int maxXPValue;

    public int Evaluate(int currentXValue)
    {
        return Evaluate(currentXValue, minXPValue, maxXPValue, 1, StaticVariables.MaxLevel);
    }
}
