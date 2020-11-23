using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class StudiesLoader : MonoBehaviour
{
	public static List<Study> studies;
	public static void Fetch()
	{
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

		studies = new List<Study>();
		foreach (var skill in skills_strings)
		{
			var study = new Study();
			study.SetCode(skill.Split(new char[] {' '})[0]);
			study.title = skill.Substring(study.GetCodeLength() + 1, skill.Length - study.GetCodeLength() - 1);
			studies.Add(study);
		}

		SetStudiesDetails();
		CreateStudies();
	}

	static void SetStudiesDetails()
	{
		var studies_details =
			Resources.Load<TextAsset>("Datasheets/StarLabs/skills").text;

		var entries = studies_details.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);

		for (var i = 0; i < entries.Length; i++) {
			/// <summary>
			/// [0] = Code
			/// [1] = Title
			/// [2] = Description
			/// [3] = Requirements
			/// </summary>
			var entry = entries[i].Split(new char[] { ',' });

			foreach (var study in studies)
			{
				if (study.GetCode() == entry[0])
				{
					study.description = entry[2];
					study.GetRequirements().Add(entry[3]);
				}
			}
		}
	}

	static void CreateStudies()
	{
		var current_root = studies[0];

		for (var i = 0; i < studies.Count; i++)
		{
			if (i + 1 >= studies.Count)
			{
				break;
			}

			var study = studies[i];
			var next_study = studies[i + 1];

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
}