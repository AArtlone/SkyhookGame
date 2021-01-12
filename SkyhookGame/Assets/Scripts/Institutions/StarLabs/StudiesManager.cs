using MyUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudiesManager : Singleton<StudiesManager>, ISavable<StudiesSaveData>
{
	public Action onInitialized;

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

	private void InitializeMethod()
	{
		var settlementData = PlayerDataManager.Instance.PlayerData.GetSettlementData(Settlement.Instance.Planet);

		if (settlementData == null)
			return;

		if (settlementData.studiesSaveData == null)
			return;

		print(settlementData.studiesSaveData);

		SetSavableData(settlementData.studiesSaveData);

		onInitialized?.Invoke();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Settlement.Instance.SetTestResourcesAmount(1000);

			if (!CompletedStudies.Contains(StudyCode.B))
				CompletedStudies.Add(StudyCode.B);
		}
	}

	public StudiesSaveData CreatSaveData()
	{
		return new StudiesSaveData(CompletedStudies);
	}

	public void SetSavableData(StudiesSaveData data)
	{
		CompletedStudies = new List<StudyCode>(data.completedStudies);
	}

	public StudiesSaveData CreateSaveData()
	{
		throw new NotImplementedException();
	}
}

[System.Serializable]
public class StudiesSaveData
{
	public List<StudyCode> completedStudies;

	public StudiesSaveData(List<StudyCode> completedStudies)
	{
		this.completedStudies = completedStudies;
	}
}
