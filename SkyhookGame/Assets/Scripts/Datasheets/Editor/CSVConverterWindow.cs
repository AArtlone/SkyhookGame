using UnityEditor;
using UnityEngine;

public class CSVConverterWindow : EditorWindow
{
    string modelName;

    [MenuItem("Window/Example")]
    public static void ShowTest()
    {
        GetWindow<CSVConverterWindow>();
    }

    private void OnGUI()
    {
        GUILayout.Label("DataModel Generator", EditorStyles.boldLabel);
        GUILayout.Space(5f);

        modelName = EditorGUILayout.TextField("DataModel Name", modelName);
        GUILayout.Space(5f);

        if (GUILayout.Button("Test"))
        {
            DSModelManager.GenerateModel(modelName);
        }
    }
}
