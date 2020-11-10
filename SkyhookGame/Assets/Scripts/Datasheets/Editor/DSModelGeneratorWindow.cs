using UnityEditor;
using UnityEngine;

public class DSModelGeneratorWindow : EditorWindow
{
    string modelName;

    [MenuItem("Window/DSModelGenerator")]
    public static void ShowWindow()
    {
        GetWindow<DSModelGeneratorWindow>();
    }

    private void OnGUI()
    {
        GUILayout.Label("DataModel Generator", EditorStyles.boldLabel);
        GUILayout.Space(5f);

        modelName = EditorGUILayout.TextField("DataModel Name", modelName);
        GUILayout.Space(5f);

        if (GUILayout.Button("Generate Model Classes")) 
            DSModelManager.GenerateModel(modelName);
    }
}
