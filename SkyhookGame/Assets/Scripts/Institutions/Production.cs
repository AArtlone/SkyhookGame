using UnityEngine;

public class Production : Institution<ProductionData>
{
    [SerializeField] private Vector2Int incremetalRateRange = default;

    private int incrementalRate;

    #region Institution Overrides
    protected override ProductionData GetInstitutionSaveData()
    {
        throw new System.NotImplementedException();
    }

    public override ProductionData CreatSaveData()
    {
        throw new System.NotImplementedException();
    }

    public override void SetSavableData(ProductionData data)
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateVariables()
    {
        base.UpdateVariables();

        incrementalRate = LevelModule.Evaluate(incremetalRateRange);
    }

    protected override void DebugVariables()
    {
    }
    #endregion
}

[System.Serializable]
public class ProductionData : InstitutionData
{
    public ProductionData(int institutionLevel)
    {
        this.institutionLevel = institutionLevel;
    }
}
