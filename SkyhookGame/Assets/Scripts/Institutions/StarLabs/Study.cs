using System;
using System.Collections.Generic;

[Serializable]
public class Study
{
	public StudyType studyType;

	/// <summary>
	/// Study characteristics
	/// </summary>
	public string title;
	public string description;
	// As in if you can start researching it.
	public bool locked = true;
	// As in if you can start researching it.
	private bool available = false;
	private string code;
	private List<string> requirements = new List<string>();

	/// <summary>
	/// Study relationships
	/// </summary>
	private Study parent_study = default;
	[NonSerialized]
	private List<Study> studies = new List<Study>();

	/// <summary>
	/// Root study, meaning it doesn't have a parent
	/// </summary>
	public Study() {}

	public Study(Study parent_study)
	{
		this.parent_study = parent_study;
	}

	/// <summary>
	/// Adds a study as a child study of this one.
	/// This creates a relationship where this study has to be
	/// unlocked before any of its children can be unlocked.
	/// </summary>
	/// <param name="study"></param>
	public void Add(Study study)
	{
		studies.Add(study);
	}

	/// <summary>
	/// Returns the number of children studies this study has.
	/// </summary>
	/// <returns></returns>
	public int GetChildrenCount()
	{
		return studies.Count;
	}

	public List<Study> GetChildren()
	{
		return studies;
	}

	public string GetCode()
	{
		return code;
	}

	public int GetCodeLength()
	{
		return code.Length;
	}

	public void SetCode(string value)
	{
		code = value;
	}

	public bool GetAvailability()
	{
		return available;
	}

	public Study GetParentStudy()
	{
		return parent_study;
	}

	public void SetParentStudy(Study study)
	{
		parent_study = study;
	}

	public List<string> GetRequirements()
	{
		return requirements;
	}
}
