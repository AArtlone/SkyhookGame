using System.IO;
using UnityEditor;
using UnityEngine;

public class ShipsDSEditorMenu : EditorWindow
{
	private const string CSVPath = "Datasheets/Ships";
	private const string ModelPath = "DSModels/ShipsModel.asset";

	[MenuItem("Window/ShipsDSModel/GenerateModel")]
	public static void GenerateModel()
	{
		string path = "Assets/Resources/" + ModelPath;
		bool exists = File.Exists(path);

		if (exists)
		{
			Debug.LogWarning("Model already exists at " + path);
			return;
		}

		ShipsDSModel model = CreateInstance<ShipsDSModel>();

		AssetDatabase.CreateAsset(model, path);
		AssetDatabase.SaveAssets();

		UpdateModel();
	}

	[MenuItem("Window/ShipsDSModel/UpdateModel")]
	public static void UpdateModel()
	{
		string path = ModelPath.Split(new char [] {'.'})[0];

		ShipsDSModel model = Resources.Load<ShipsDSModel>(path);

		if (model == null)
			return;

		model.Initialize(CSVPath);
	}
}
