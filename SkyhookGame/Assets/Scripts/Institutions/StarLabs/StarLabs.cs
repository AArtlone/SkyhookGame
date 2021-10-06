using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLabs : Institution<StarLabsData>
{
    [SerializeField] private Vector2Int tasksCapacityRange = default;

    private List<string> unlockedStudies = new List<string>();

    private int tasksCapacity;

    private IEnumerator Start()
    {
        yield return SceneLoader.Instance.WaitForLoading();

        InitializeMethod();
    }

    public void UnlockStudy(string type)
    {
		StudiesManager.Instance.UnlockStudy(
			(StudyCode)Enum.Parse(typeof(StudyCode), type)
		);
    }

    #region Institution Overrides
    protected override StarLabsData GetInstitutionSaveData()
    {
        var playerData = PlayerDataManager.Instance.PlayerData;
        SettlementData settlementData = null;

        if (playerData != null)
            settlementData = playerData.GetSettlementData(Settlement.Instance.Planet);

        var data = settlementData == null ? null : settlementData.starLabsData;

        return data;
    }

    public override StarLabsData CreateSaveData()
    {
        var saveData = new StarLabsData(LevelModule.Level, unlockedStudies);
        return saveData;
    }

    public override void SetSavableData(StarLabsData data)
    {
        unlockedStudies = data.unlockedStudies;
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
    public List<string> unlockedStudies;

    public StarLabsData(int institutionLevel, List<string> unlockedStudies)
    {
        this.institutionLevel = institutionLevel;
        this.unlockedStudies = unlockedStudies;
    }
}
