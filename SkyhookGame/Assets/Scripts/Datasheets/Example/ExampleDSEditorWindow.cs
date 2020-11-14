using UnityEditor;
using UnityEngine;

public class ExampleDSEditorWindow : EditorWindow
{
	private ExampleDSModel model;

	[MenuItem("Window/ExampleDSModelGenerator")]
	public static void ShowWindow()
	{
		GetWindow<ExampleDSEditorWindow>();
	}

	private void OnGUI()
	{
		GUILayout.Space(10f);
		if (GUILayout.Button("GenerateModel"))
			GenerateModel();

		GUILayout.Space(10f);
		if (GUILayout.Button("UpdateModel"))
			UpdateModel();
	}

	private void GenerateModel()
	{
		model = CreateInstance<ExampleDSModel>();

		AssetDatabase.CreateAsset(model, "Assets/Scripts/Datasheets/Example/ExampleModel.asset");
		AssetDatabase.SaveAssets();

		UpdateModel();
	}

	private void UpdateModel()
	{
		if (model == null)
			return;

		model.Initialize("Datasheets/Example");
	}
}
