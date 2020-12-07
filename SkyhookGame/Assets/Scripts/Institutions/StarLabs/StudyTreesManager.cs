using MyUtilities;
using System.Collections.Generic;
using UnityEngine;

public class StudyTreesManager : Singleton<StudyTreesManager>
{
	[SerializeField] private StudiesSO studiesSO = default;

    protected override void Awake()
	{
		SetInstance(this);

		if (studiesSO == null)
		{
			enabled = false;
			Debug.LogError($"StudiesSO is not assigned in the editor or null on {gameObject.name}");
			return;
		}
	}

	public List<Study> GetStudiesByType(StudyType studyType)
	{
		List<Study> result = new List<Study>();

		foreach (var study in studiesSO.allStudies)
		{
			// TODO: make sure study has a StudyType
			if (studyType != study.studyType)
				continue;

			result.Add(study);
		}

		return result;
	}
}
