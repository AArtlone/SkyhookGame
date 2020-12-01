using System.IO;
using UnityEditor;
using UnityEngine;

public class ShipsDSEditorMenu : EditorWindow
{
	private const string CSVPath = "Datasheets/Ships";
	private const string ModelPath = "DSModels/ShipsModel.asset";

	private static ShipsDSModel model;

	[MenuItem("Window/ShipsDSModel/GenerateModel")]
	public static void GenerateModel()
	{
		string path = "Assets/ScriptableObjects/" + ModelPath;
		bool exists = File.Exists(path);

		if (exists)
		{
			Debug.LogWarning("Model already exists at " + path);
			return;
		}

		model = CreateInstance<ShipsDSModel>();

		AssetDatabase.CreateAsset(model, path);
		AssetDatabase.SaveAssets();

		UpdateModel();
	}

	[MenuItem("Window/ShipsDSModel/UpdateModel")]
	public static void UpdateModel()
	{
		if (model == null)
			return;

		model.Initialize(CSVPath);
	}
}
