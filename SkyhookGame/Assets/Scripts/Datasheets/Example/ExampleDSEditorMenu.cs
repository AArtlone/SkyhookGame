using UnityEditor;
using UnityEngine;

public class ExampleDSEditorWindow : EditorWindow
{
	private static ExampleDSModel model;

	[MenuItem("Window/ExampleDSModelGenerator/GenerateModel")]
	public static void GenerateModel()
	{
		model = CreateInstance<ExampleDSModel>();

		AssetDatabase.CreateAsset(model, "Assets/Scripts/Datasheets/Example/ExampleModel.asset");
		AssetDatabase.SaveAssets();

		UpdateModel();
	}

	[MenuItem("Window/ExampleDSModelGenerator/UpdateModel")]
	public static void UpdateModel()
	{
		if (model == null)
			return;

		model.Initialize("Datasheets/Example");
	}
}
