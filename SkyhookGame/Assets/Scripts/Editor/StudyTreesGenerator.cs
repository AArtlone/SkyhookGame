using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StudyTreesGenerator : MonoBehaviour
{
	// Tabs
	private static Transform tabsContainer;
	private static GameObject tabButtonPrefab;

	// Pages
	private static Transform pagesContainer;
	private static GameObject pagePrefab;

	// Studies
	private static List<Study> studies = new List<Study>();

	[MenuItem("Generators/Generate Study Trees")]
	static void Generate()
	{
		ChangeScene();
		ClearScene();
		LoadPrefabs();

		StudiesLoader.Fetch();
		studies = StudiesLoader.studies;

		GenerateUI();
	}

	/// <summary>
	/// Generates the UI of the tabs (buttons) and pages for each study tree to the star labs view.
	/// </summary>
	static void GenerateUI()
	{
		for (int i = 0; i < studies.Count; i++)
		{
			if (studies[i].GetCodeLength() == 1)
			{
				// Tab button
				var tabButton = Instantiate(tabButtonPrefab, tabsContainer);
				var textComponent = tabButton.GetComponentInChildren<TextMeshProUGUI>();
				tabButton.name = "TabButton_" + studies[i].title;
				textComponent.text = studies[i].title;

				// Study tree page
				var studyTreePage = Instantiate(pagePrefab, pagesContainer);
				studyTreePage.name = "Page_" + studies[i].title;
				studyTreePage.SetActive(false);
				GetTree(studies[i], 1);
			}
		}
	}
	
	static void LoadPrefabs()
	{
		if (tabButtonPrefab == null)
		{
			tabButtonPrefab = Resources.Load<GameObject>("Prefabs/StarLabs/TabButton");
		}
		if (pagePrefab == null)
		{
			pagePrefab = Resources.Load<GameObject>("Prefabs/StarLabs/StudiesTreeView");
		}
	}

	static void GetTree(Study tree, int depth)
	{
		foreach (var study in tree.GetChildren())
		{
			GetChildren(study, depth);
		}
	}

	static void GetChildren(Study study, int depth)
	{
		if (study.GetChildrenCount() == 0)
		{
			return;
		}
		if (study.GetChildrenCount() > 0)
		{
			GetTree(study, ++depth);
		}
	}

	static void LoadStudysIntoTrees()
	{

	}

	static void ChangeScene()
	{
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		EditorSceneManager.OpenScene("Assets/Scenes/StarLabs.unity");
	}

	static void ClearScene()
	{
		if (tabsContainer == null)
		{
			tabsContainer =
				GameObject.FindGameObjectWithTag("TabGroup").transform;
		}
		if (pagesContainer == null)
		{
			pagesContainer =
				GameObject.FindGameObjectWithTag("Pages").transform;
		}

		for(int i = tabsContainer.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(tabsContainer.GetChild(i).gameObject);
		}
		for(int i = pagesContainer.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(pagesContainer.GetChild(i).gameObject);
		}
	}
}
