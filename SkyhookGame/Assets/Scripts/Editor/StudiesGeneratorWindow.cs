using System.IO;
using UnityEditor;
using UnityEngine;

public class StudiesGeneratorWindow : EditorWindow
{
	private const string ModelPath = "Studies/StudiesModel.asset";

	[MenuItem("Window/Studies/GenerateModel")]
	public static void GenerateModel()
	{
		string path = "Assets/Resources/" + ModelPath;
		bool exists = File.Exists(path);

		if (exists)
		{
			Debug.LogWarning("Model already exists at " + path);
			return;
		}

		StudiesSO model = CreateInstance<StudiesSO>();

		AssetDatabase.CreateAsset(model, path);
		AssetDatabase.SaveAssets();

		UpdateModel();
	}

	[MenuItem("Window/Studies/UpdateModel")]
	public static void UpdateModel()
	{
		string path = ModelPath.Split(new char[] { '.' })[0];

		StudiesSO model = Resources.Load<StudiesSO>(path);

		if (model == null)
			return;

		model.Initialize();
	}
}
