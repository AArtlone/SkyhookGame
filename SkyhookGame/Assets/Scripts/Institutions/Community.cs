using UnityEngine;

public class Community : Institution<CommunityData>
{
    [SerializeField] private Vector2Int humansRange = default;
    
    public int TotalHumans { get; private set; }
    public int AvailableHumans { get; private set; }

    #region Institution Overrides
    protected override void InitializeMethod()
    {
        base.InitializeMethod();

        AvailableHumans = TotalHumans;
    }

    protected override CommunityData GetInstitutionSaveData()
    {
        throw new System.NotImplementedException();
    }

    public override CommunityData CreatSaveData()
    {
        throw new System.NotImplementedException();
    }

    public override void SetSavableData(CommunityData data)
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateVariables()
    {
        base.UpdateVariables();

        int previousTotalHumans = TotalHumans;
        TotalHumans = LevelModule.Evaluate(humansRange);
        AvailableHumans += TotalHumans - previousTotalHumans;
    }

    protected override void DebugVariables()
    {
        Debug.Log("New Total Humans number = " + TotalHumans);
        Debug.Log("New Available Humans number = " + AvailableHumans);
    } 
    #endregion

    /// <summary>
    /// Checks if there are enough available humans
    /// </summary>
    public bool CheckIfCanPerform(int requiredAmount)
    {
        return AvailableHumans >= requiredAmount;
    }
}

[System.Serializable]
public class CommunityData : InstitutionData
{
    public CommunityData(int institutionLevel)
    {
        this.institutionLevel = institutionLevel;
    }
}
