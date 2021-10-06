using MyUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudiesManager : Singleton<StudiesManager>, ISavable<StudiesSaveData>
{
	public Action onInitialized;
	public Action<StudyCode> onStudyCompleted;

	[HideInInspector] public List<StudyCode> CompletedStudies = new List<StudyCode>();

	protected override void Awake()
	{
		SetInstance(this);

		//CompletedStudies.Add(StudyCode.B);
	}

	private IEnumerator Start()
	{
		yield return SceneLoader.Instance.WaitForLoading();

		InitializeMethod();
	}

	// This is a function (testing github webhook to discord)
    private void InitializeMethod()
	{
		if (PlayerDataManager.Instance.PlayerData == null)
			return;

		var settlementData = PlayerDataManager.Instance.PlayerData.GetSettlementData(Settlement.Instance.Planet);

		if (settlementData == null)
			return;

		if (settlementData.studiesSaveData == null)
			return;

		SetSavableData(settlementData.studiesSaveData);

		onInitialized?.Invoke();
	}

	public void UnlockStudy(StudyCode type)
	{
		if (!CompletedStudies.Contains(type))
        {
			CompletedStudies.Add(type);
			onStudyCompleted?.Invoke(type);
		}
	}

	public bool CheckIfStudyIsUnlocked(string type)
	{
		return CompletedStudies.Contains((StudyCode)Enum.Parse(typeof(StudyCode), type));
	}

	public StudiesSaveData CreatSaveData()
	{
		return new StudiesSaveData(CompletedStudies);
	}

	public void SetSavableData(StudiesSaveData data)
	{
		CompletedStudies = new List<StudyCode>(data.CompletedStudies);
	}

	public StudiesSaveData CreateSaveData()
	{
		throw new NotImplementedException();
	}
}

[System.Serializable]
public class StudiesSaveData
{
	public List<StudyCode> CompletedStudies;

	public StudiesSaveData(List<StudyCode> CompletedStudies)
	{
		this.CompletedStudies = CompletedStudies;
	}
}
