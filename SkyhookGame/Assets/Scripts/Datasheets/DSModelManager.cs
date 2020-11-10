using System.IO;
using UnityEditor;
using UnityEngine;

public class DSModelManager : MonoBehaviour
{
    private const string DatasheetsPath = "Assets/Resources/Datasheets/";

    private const string DatasheetsModelsPath = "Assets/Scripts/Datasheets";

    private const string ModelFileName = "DSModel";
    private const string RecordFileName = "DSRecord";
    private const string IDFileName = "DSID";
    private const string EditorWindowFileName = "DSEditorWindow";

    public static void GenerateModel(string modelName)
    {
        if (!CheckCSVFile(modelName))
            return;

        CreateDSModelFolder(modelName);

        GenerateClasses(modelName);

        AssetDatabase.Refresh();
    }

    private static void GenerateClasses(string modelName)
    {
        GenerateModelClass(modelName);

        GenerateRecordClass(modelName);

        GenerateIDClass(modelName);

        GenerateEditorWindowClass(modelName);
    }

    private static void CreateDSModelFolder(string modelName)
    {
        AssetDatabase.CreateFolder(DatasheetsModelsPath, modelName);
    }

    private static void GenerateModelClass(string modelName)
    {
        string modelFilePath = GetFilePath(modelName, ModelFileName);

        Debug.Log("Creating Classfile: " + modelFilePath);

        if (File.Exists(modelFilePath))
        {
            Debug.LogWarning($"The {modelName}DSModel class already exists");
            return;
        }

        using (StreamWriter outFile = new StreamWriter(modelFilePath))
        {
            outFile.WriteLine("using UnityEngine;");
            outFile.WriteLine("using System.Collections;");
            outFile.WriteLine("");
            outFile.WriteLine("[CreateAssetMenu(fileName=\"Model\", menuName=\"Datasheets/Model\")]");
            outFile.WriteLine($"public class {modelName}DSModel: DSModelBase<{modelName}DSRecord, {modelName}DSID>");
            outFile.WriteLine("{");
            outFile.WriteLine("");
            outFile.WriteLine("}");
        }
    }

    private static void GenerateRecordClass(string modelName)
    {
        string recordFilePath = GetFilePath(modelName, RecordFileName);

        Debug.Log("Creating Classfile: " + recordFilePath);

        if (File.Exists(recordFilePath))
        {
            Debug.LogWarning($"The {modelName}DSRecord class already exists");
            return;
        }

        using (StreamWriter outFile = new StreamWriter(recordFilePath))
        {
            outFile.WriteLine("using System;");
            outFile.WriteLine("");
            outFile.WriteLine("[Serializable]");
            outFile.WriteLine($"public class {modelName}DSRecord: DSRecordBase<{modelName}DSID>");
            outFile.WriteLine("{");
            outFile.WriteLine("");
            outFile.WriteLine("}");
        }
    }

    private static void GenerateIDClass(string modelName)
    {
        string idFilePath = GetFilePath(modelName, IDFileName);

        Debug.Log("Creating Classfile: " + idFilePath);

        if (File.Exists(idFilePath))
        {
            Debug.LogWarning($"The {modelName}DSID class already exists");
            return;
        }

        using (StreamWriter outFile = new StreamWriter(idFilePath))
        {
            outFile.WriteLine($"public enum {modelName}DSID");
            outFile.WriteLine("{");
            outFile.WriteLine("");
            outFile.WriteLine("}");
        }
    }

    private static void GenerateEditorWindowClass(string modelName)
    {
        string idFilePath = GetFilePath(modelName, EditorWindowFileName);

        Debug.Log("Creating Classfile: " + idFilePath);

        if (File.Exists(idFilePath))
        {
            Debug.LogWarning($"The {modelName}DSID class already exists");
            return;
        }

        using (StreamWriter outFile = new StreamWriter(idFilePath))
        {
            outFile.WriteLine("using UnityEditor;");
            outFile.WriteLine("using UnityEngine;");
            
            outFile.WriteLine("");
            
            outFile.WriteLine($"public class {modelName}DSEditorWindow : EditorWindow");
            outFile.WriteLine("{");
            
            outFile.WriteLine($"\t[MenuItem(\"Window/{modelName}DSModelGenerator\")]");
            outFile.WriteLine("\tpublic static void ShowWindow()");
                outFile.WriteLine("\t{");
                outFile.WriteLine($"\t\tGetWindow<{modelName}DSEditorWindow>();");
                outFile.WriteLine("\t}");

            outFile.WriteLine("");

            outFile.WriteLine("\tprivate void OnGUI()");
            outFile.WriteLine("\t{");
            outFile.WriteLine("\t\tGUILayout.Space(10f);");
            outFile.WriteLine($"\t\tif (GUILayout.Button(\"GenerateModel\"))");
            outFile.WriteLine("\t\t\tGenerateModel();");
            outFile.WriteLine("");
            outFile.WriteLine("\t\tGUILayout.Space(10f);");
            outFile.WriteLine($"\t\tif (GUILayout.Button(\"UpdateModel\"))");
            outFile.WriteLine("\t\t\tUpdateModel();");
            outFile.WriteLine("\t}");

            outFile.WriteLine("");
            
            outFile.WriteLine("\tprivate void GenerateModel()");
                outFile.WriteLine("\t{");
                outFile.WriteLine($"\t\t{modelName}DSModel model = CreateInstance<{modelName}DSModel>();");
                outFile.WriteLine("");
                outFile.WriteLine($"\t\tAssetDatabase.CreateAsset(model, \"Assets/Scripts/Datasheets/{modelName}/{modelName}Model.asset\");");
                outFile.WriteLine($"\t\tAssetDatabase.SaveAssets();");
                outFile.WriteLine("\t}");

            outFile.WriteLine("");

            outFile.WriteLine("\tprivate void UpdateModel()");
            outFile.WriteLine("\t{");
            outFile.WriteLine("");
            outFile.WriteLine("\t}");

            outFile.WriteLine("}");
        }
    }

    private static string GetFilePath(string modelName, string fileName)
    {
        return $"Assets/Scripts/Datasheets/{modelName}/{modelName}{fileName}.cs";
    }

    private static bool CheckCSVFile(string modelName)
    {
        bool exists = File.Exists(DatasheetsPath + $"{modelName}.csv");

        if (!exists)
            Debug.LogWarning($"The CSV file with name {modelName} was not found in the Resources/Datasheets. Please make sure the file exists and has the correct name and file extension");

        return exists;
    }
}
