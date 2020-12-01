using System.IO;
using UnityEditor;
using UnityEngine;

public class ResourcesDSEditorMenu : EditorWindow
{
	private const string CSVPath = "Datasheets/Resources";
	private const string ModelPath = "DSModels/ResourcesModel.asset";

	private static ResourcesDSModel model;

	[MenuItem("Window/ResourcesDSModel/GenerateModel")]
	public static void GenerateModel()
	{
		string path = "Assets/ScriptableObjects/" + ModelPath;
		bool exists = File.Exists(path);

		if (exists)
		{
			Debug.LogWarning("Model already exists at " + path);
			return;
		}

		model = CreateInstance<ResourcesDSModel>();

		AssetDatabase.CreateAsset(model, path);
		AssetDatabase.SaveAssets();

		UpdateModel();
	}

	[MenuItem("Window/ResourcesDSModel/UpdateModel")]
	public static void UpdateModel()
	{
		if (model == null)
			return;

		model.Initialize(CSVPath);
	}
}
