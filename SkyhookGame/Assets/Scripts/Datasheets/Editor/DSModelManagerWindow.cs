using UnityEditor;
using UnityEngine;

public class DSModelManagerWindow : EditorWindow
{
    string modelName;

    [MenuItem("Window/DSModelManager")]
    public static void ShowWindow()
    {
        GetWindow<DSModelManagerWindow>();
    }

    private void OnGUI()
    {
        GUILayout.Label("DSModel Manager", EditorStyles.boldLabel);
        GUILayout.Space(5f);

        GUILayout.Label("Rule: Make sure that the CSV file is put into Resources/Datasheets, and make sure the Model Name is the same as the CSV file name", EditorStyles.textArea);
        GUILayout.Space(5f);

        modelName = EditorGUILayout.TextField("Model Name", modelName);
        GUILayout.Space(10f);

        if (GUILayout.Button("Generate Model Classes")) 
            DSModelManager.GenerateModelClasses(modelName);
    }
}
