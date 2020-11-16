using UnityEditor;
using UnityEngine;

public class ExampleDSEditorWindow : EditorWindow
{
	private const string CSVPath = "Datasheets/Example";
	private const string ModelPath = "DSModels/ExampleModel";

	[MenuItem("Window/ExampleDSModel/GenerateModel")]
	public static void GenerateModel()
	{
		ExampleDSModel model = CreateInstance<ExampleDSModel>();

		AssetDatabase.CreateAsset(model, "Assets/Resources/" + ModelPath + ".asset");
		AssetDatabase.SaveAssets();

		UpdateModel();
	}

	[MenuItem("Window/ExampleDSModel/UpdateModel")]
	public static void UpdateModel()
	{
		ExampleDSModel model = Resources.Load<ExampleDSModel>(ModelPath);

		if (model == null)
			return;

		model.Initialize(CSVPath);
	}
}
