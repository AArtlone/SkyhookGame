using UnityEngine;

public class HumanSector : Institution
{
    public int TotalHumans { get; private set; }
    public int AvailableHumans { get; private set; }

    private HumanSectorLevelUpSO levelUpSO;

    public override void Upgrade()
    {
        base.Upgrade();

        int previousTotalHumans = TotalHumans;

        UpdateVariables();

        AvailableHumans += TotalHumans - previousTotalHumans;

        DebugVariables();
    }

    protected override void InitializeMethod()
    {
        levelUpSO = Resources.Load<HumanSectorLevelUpSO>("ScriptableObjects/HumanSectorLevelUpSO");

        UpdateVariables();

        AvailableHumans = TotalHumans;

        DebugVariables();
    }

    protected override void UpdateVariables()
    {
        TotalHumans = levelUpSO.Evaluate(Level);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New Total Humans number = " + TotalHumans);
        Debug.Log("New Available Humans number = " + AvailableHumans);
    }

    /// <summary>
    /// Checks if there are enough available humans
    /// </summary>
    public bool CheckIfCanPerform(int requiredAmount)
    {
        return AvailableHumans >= requiredAmount;
    }
}
