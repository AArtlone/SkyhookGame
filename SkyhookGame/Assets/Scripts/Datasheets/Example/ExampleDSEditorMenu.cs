using System.IO;
using UnityEditor;
using UnityEngine;

public class ExampleDSEditorMenu : EditorWindow
{
	private const string CSVPath = "Datasheets/Example";
	private const string ModelPath = "DSModels/ExampleModel.asset";

	[MenuItem("Window/ExampleDSModel/GenerateModel")]
	public static void GenerateModel()
	{
		string path = "Assets/Resources/" + ModelPath;
		bool exists = File.Exists(path);

		if (exists)
		{
			Debug.LogWarning("Model already exists at " + path);
			return;
		}

		ExampleDSModel model = CreateInstance<ExampleDSModel>();

		AssetDatabase.CreateAsset(model, path);
		AssetDatabase.SaveAssets();

		UpdateModel();
	}

	[MenuItem("Window/ExampleDSModel/UpdateModel")]
	public static void UpdateModel()
	{
		string path = ModelPath.Split(new char [] {'.'})[0];

		ExampleDSModel model = Resources.Load<ExampleDSModel>(path);

		if (model == null)
			return;

		model.Initialize(CSVPath);
	}
}
