using System.Collections.Generic;
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

    public override ProductionData CreateSaveData()
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
	/// <summary>
	/// The current state (amount) of each resource type
	/// </summary>
	private List<Resource> resources;

    public ProductionData(int institutionLevel, List<Resource> resources)
    {
        this.institutionLevel = institutionLevel;
		this.resources = resources;
    }
}
