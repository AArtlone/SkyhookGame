using System;
using System.IO;
using System.Threading;
using UnityEngine;

public class IOFileHandler
{
    private string dataPath = default;

    public IOFileHandler()
    {
        dataPath = Application.dataPath;
    }

    public void SaveFile(string jsonData, string fileName, Action<bool> callback)
    {
        var savingThread = new Thread(() => 
        { 
            WriteFile(fileName, jsonData, callback); 
        });

        savingThread.Start();
    }

    public void LoadFile(string fileName, Action<string> callback)
    {
        var thread = new Thread(() =>
        {
            ReadFile(callback, fileName);
        });

        thread.Start();
    }

    public void CheckFileExists(string fileName, Action<bool> callback)
    {
        string filePath = GetFilePath(fileName);
        
        bool exists = false;

        try
        {
            exists = File.Exists(filePath);
        }
        catch(Exception exception)
        {
            if (Application.isEditor)
                throw exception;
        }

        callback(exists);
    }

    private void ReadFile(Action<string> callback, string fileName)
    {
        string path = GetFilePath(fileName);

        string data = null;

        try
        {
            data = File.ReadAllText(path);
        }
        catch(Exception exception)
        {
            if (Application.isEditor)
                throw exception;
        }

        callback(data);
    }

    private void WriteFile(string fileName, string data, Action<bool> doneCallback)
    {
        string path = GetFilePath(fileName);

        // Converting to JSON
        path += ".json";

        try
        {
            File.WriteAllText(path, data, System.Text.Encoding.UTF8);
        }
        catch (Exception exception)
        {
            if (Application.isEditor)
                Debug.LogException(exception);

            doneCallback(false);

            return;
        }

        doneCallback(true);
    }

    private string GetFilePath(string fileName)
    {
        if (Application.isEditor)
        {
            string editorPath = Path.GetDirectoryName(dataPath);

            editorPath += "/SaveData";

            if (!Directory.Exists(editorPath))
                Directory.CreateDirectory(editorPath);

            return Path.Combine(editorPath, fileName);
        }

        return Path.Combine(Application.persistentDataPath, fileName);
    }
}
