using System.IO;
using UnityEditor;
using UnityEngine;

public class DSModelManager : MonoBehaviour
{
    private const string DatasheetsFolderPath = "Assets/Resources/Datasheets/";

    public static void GenerateModel(string modelName)
    {
        if (!CheckCSVFile(modelName))
            return;

        string filePath = $"Assets/Scripts/Datasheets/{modelName}DSModel.cs";

        Debug.Log("Creating Classfile: " + filePath);

        if (File.Exists(filePath))
        {
            Debug.LogWarning($"The {modelName}DSModel class already exists");
            return;
        }
        using (StreamWriter outfile = new StreamWriter(filePath))
        {
            GenerateModelClass(outfile, modelName);

            // Generating Record class
            outfile.WriteLine(" ");
            GenerateRecordClass(outfile, modelName);
            
            // Generating enum file
            outfile.WriteLine(" ");
            GenerateIDClass(outfile, modelName);
        }

        AssetDatabase.Refresh();
    }

    private static void GenerateModelClass(StreamWriter outFile, string modelName)
    {
        outFile.WriteLine("using UnityEngine;");
        outFile.WriteLine("using System.Collections;");
        outFile.WriteLine("");
        outFile.WriteLine($"public class {modelName}DSModel: DSModelBase<{modelName}DSRecord, {modelName}DSID>");
        outFile.WriteLine("{");
        outFile.WriteLine("");
        outFile.WriteLine("}");
    }

    private static void GenerateRecordClass(StreamWriter outFile, string modelName)
    {
        outFile.WriteLine($"public class {modelName}DSRecord: DSRecordBase");
        outFile.WriteLine("{");
        outFile.WriteLine("");
        outFile.WriteLine("}");
    }

    private static void GenerateIDClass(StreamWriter outFile, string modelName)
    {
        outFile.WriteLine($"public enum {modelName}DSID");
        outFile.WriteLine("{");
        outFile.WriteLine("");
        outFile.WriteLine("}");
    }


    private static bool CheckCSVFile(string modelName)
    {
        bool exists = File.Exists(DatasheetsFolderPath + $"{modelName}.csv");

        if (!exists)
            Debug.LogWarning($"The CSV file with name {modelName} was not found in the Resources/Datasheets. Please make sure the file exists and has the correct name and file extension");

        return exists;
    }
}
