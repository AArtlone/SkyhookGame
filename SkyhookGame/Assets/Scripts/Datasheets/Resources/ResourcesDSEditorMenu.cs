using System.IO;
using UnityEditor;
using UnityEngine;

public class ResourcesDSEditorMenu : EditorWindow
{
	private const string CSVPath = "Datasheets/Resources";
	private const string ModelPath = "DSModels/ResourcesModel.asset";

	[MenuItem("Window/ResourcesDSModel/GenerateModel")]
	public static void GenerateModel()
	{
		string path = "Assets/Resources/" + ModelPath;
		bool exists = File.Exists(path);

		if (exists)
		{
			Debug.LogWarning("Model already exists at " + path);
			return;
		}

		ResourcesDSModel model = CreateInstance<ResourcesDSModel>();

		AssetDatabase.CreateAsset(model, path);
		AssetDatabase.SaveAssets();

		UpdateModel();
	}

	[MenuItem("Window/ResourcesDSModel/UpdateModel")]
	public static void UpdateModel()
	{
		string path = ModelPath.Split(new char [] {'.'})[0];

		ResourcesDSModel model = Resources.Load<ResourcesDSModel>(path);

		if (model == null)
			return;

		model.Initialize(CSVPath);
	}
}
