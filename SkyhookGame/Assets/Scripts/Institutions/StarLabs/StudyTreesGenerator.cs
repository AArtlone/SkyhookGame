using System.Collections.Generic;
using UnityEngine;

public class StudyTreesGenerator : MonoBehaviour
{
	[SerializeField] private StudiesSO studiesSO = default;

	[Space(5f)]
	[SerializeField] private StudySelectableController selectableController = default;
	[SerializeField] private StarLabsTabGroup tabGroup = default;

    private void Awake()
    {
        if (studiesSO == null)
        {
			enabled = false;
			Debug.LogError($"StudiesSO is not assigned in the editor or null on {gameObject.name}");
			return;
        }
    }

    private void Start()
	{
		tabGroup.onTabSelection += TabGroup_OnTabChange;
	}

	private void TabGroup_OnTabChange(StudyType type)
    {
		var studiesToShow = GetStudiesByType(type);

		SetStudiesDataSet(studiesToShow);
    }

	private List<Study> GetStudiesByType(StudyType studyType)
	{
		List<Study> result = new List<Study>();

		foreach (var study in studiesSO.allStudies)
		{
			// TODO: make sure study has a StudyType
			if (studyType.ToString() != study.title)
				continue;

			result.Add(study);
		}

		return result;
	}

	private void SetStudiesDataSet(List<Study> studies)
	{
		List<StudyCellData> dataSet = new List<StudyCellData>(studies.Count);

		studies.ForEach(s => dataSet.Add(new StudyCellData(s)));

		selectableController.SetDataSet(dataSet);
	}
}
