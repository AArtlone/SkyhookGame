using UnityEngine;

public class StarLabs : Institution<StarLabsData>
{
    [SerializeField] private Vector2Int tasksCapacityRange = default;

    private int tasksCapacity;

    public void UnlockStudy(Study study)
    {

    }

    #region Institution Overrides
    protected override StarLabsData GetInstitutionSaveData()
    {
        throw new System.NotImplementedException();
    }

    public override StarLabsData CreatSaveData()
    {
        throw new System.NotImplementedException();
    }

    public override void SetSavableData(StarLabsData data)
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateVariables()
    {
        base.UpdateVariables();

        tasksCapacity = LevelModule.Evaluate(tasksCapacityRange);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New RD tasks capacity = " + tasksCapacity);
    }
    #endregion
}

[System.Serializable]
public class StarLabsData : InstitutionData
{
    public StarLabsData(int institutionLevel)
    {
        this.institutionLevel = institutionLevel;
    }
}
