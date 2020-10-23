using UnityEngine;

public class Community : Institution
{
    [SerializeField] private Vector2Int humansRange = default;
    
    public int TotalHumans { get; private set; }
    public int AvailableHumans { get; private set; }

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
        UpdateVariables();

        AvailableHumans = TotalHumans;

        DebugVariables();
    }

    protected override void UpdateVariables()
    {
        TotalHumans = LevelModule.Evaluate(humansRange);
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
