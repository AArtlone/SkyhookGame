using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class StudiesModel : ScriptableObject
{
	public List<Study> allStudies;

	public void Initialize()
	{
		allStudies = new List<Study>();
		var csv_data =
			Resources.Load<TextAsset>("Datasheets/StarLabs/skill_trees_test_sheet").text;

		var regex_skills = new Regex(
			"[(]([A-Z]+)[)][ ][A-z,][^,\r]*[0-9]?",
			RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

		var matches = regex_skills.Matches(csv_data);

		var skills_strings = new List<string>();
		foreach (Match match in matches)
		{
			var regex_filter = new Regex(
				"[()\r]",
				RegexOptions.Multiline | RegexOptions.Compiled);

			var result = regex_filter.Replace(match.Value, "");

			skills_strings.Add(result);
		}
		skills_strings.Sort();

		Study currentRoot = new Study();
		for (int i = 0; i < skills_strings.Count; i++)
		{
			Study currentStudy = new Study();
			var studyCode = skills_strings[i].Split(new char[] { ' ' })[0];
			var studyTitle = skills_strings[i].Substring(
				studyCode.Length + 1,
				skills_strings[i].Length - studyCode.Length - 1
			);

			currentStudy.SetCode(studyCode);
			currentStudy.SetTitle(studyTitle);

			if (studyCode.Length == 1)
			{
				currentRoot = currentStudy;
			}
			else if (
				studyCode.Length > 1 &&
				(studyCode.Length - 1 == allStudies[i - 1].GetCodeLength()) ||
				studyCode.Length - 1 == currentRoot.GetCodeLength())
			{
				if (studyCode.Length - 1 == allStudies[i - 1].GetCodeLength())
				{
					currentRoot = allStudies[i - 1];
				}
				currentStudy.SetParentStudy(currentRoot);
			}

			allStudies.Add(currentStudy);
		}

		SetStudiesDetails();
		CreateStudies();

		for (var i = 0; i < allStudies.Count; i++)
		{
			var study = allStudies[i];

			// We retrieve the root of this study, if it already isn't the root,
			// in order to find what type it is.
			var currentStudy = study;
			while (currentStudy.GetParentStudy() != null)
			{
				currentStudy = currentStudy.GetParentStudy();
			}

			// Creates the study type based on the root study's title
			// since that title is the study type either way.
			study.studyType = (StudyType)Enum.Parse(typeof(StudyType), currentStudy.title);
		}

		SetStudiesPortraits();
	}

	private void SetStudiesDetails()
	{
		var studies_details =
			Resources.Load<TextAsset>("Datasheets/StarLabs/skills").text;

		var entries = studies_details.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);

		for (var i = 0; i < entries.Length; i++)
		{
			/// <summary>
			/// [0] = Code
			/// [1] = Title
			/// [2] = Description
			/// [3] = Requirements
			/// </summary>
			var entry = entries[i].Split(new char[] { ',' });

			foreach (var study in allStudies)
			{
				if (study.GetCode() == entry[0])
				{
					study.description = entry[2];
					study.GetRequirements().Add(entry[3]);
				}
			}
		}
	}

	private void CreateStudies()
	{
		var current_root = allStudies[0];

		for (var i = 0; i < allStudies.Count; i++)
		{
			if (i + 1 >= allStudies.Count)
			{
				break;
			}

			var study = allStudies[i];
			var next_study = allStudies[i + 1];

			if (study.GetCodeLength() < next_study.GetCodeLength())
			{
				current_root = study;
				study.Add(next_study);
			}
			if (study.GetCodeLength() == next_study.GetCodeLength() &&
				study.GetCodeLength() != 1)
			{
				current_root.Add(next_study);
			}
			if (next_study.GetCodeLength() == 1 ||
				next_study.GetCodeLength() < study.GetCodeLength())
			{
				current_root = next_study;
			}
		}
	}

	public List<Study> GetStudiesByType(StudyType studyType)
    {
		List<Study> result = new List<Study>();

		foreach (var study in allStudies)
        {
			if (studyType != study.studyType)
				continue;

			result.Add(study);
        }

		return result;
    }

	public void SetStudiesPortraits()
	{
		var csv_data =
			Resources.Load<TextAsset>("Datasheets/StarLabs/skills").text;
		var lines = csv_data.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries); ;

		var studiesObjects = new List<Dictionary<string, string>>();
		for (int i = 1; i < lines.Length - 1; i++)
		{
			var propertiesList = lines[i].Split(',');
			var obj = new Dictionary<string, string>();

			obj.Add("Code", propertiesList[0]);
			obj.Add("Title", propertiesList[1]);
			obj.Add("Description", propertiesList[2]);
			obj.Add("Requirements", propertiesList[3]);
			obj.Add("Portrait", propertiesList[4]);

			studiesObjects.Add(obj);
		}

		for (int i = 0; i < allStudies.Count; i++)
		{
			for (int j = 0; j < studiesObjects.Count; j++)
			{
				if (allStudies[i].GetCode() == studiesObjects[j]["Code"])
				{
					allStudies[i].description = studiesObjects[j]["Description"];
					allStudies[i].portrait = studiesObjects[j]["Portrait"];
				}
			}
		}
	}
}
