using System;
using UnityEngine;

public class IOUtility : MonoBehaviour
{
    private IOFileHandler fileHandler;

    private void Start()
    {
        fileHandler = new IOFileHandler();
    }

    public void SaveData(PlayerData data, Action<bool> callback)
    {
        if (data == null)
        {
            if (Application.isEditor)
                throw new Exception("Passed data is null");

            callback(false);

            return;
        }

        string jsonData;

        if (!TryToParseToJson(data, out jsonData))
        {
            callback(false);

            return;
        }

        fileHandler.SaveFile(jsonData, "PlayerData", callback);
    }

    public void LoadData(Action<PlayerData> callback)
    {
        fileHandler.CheckFileExists("PlayerData", (bool exists) =>
        {
            if (!exists)
            {
                callback(null);
                return;
            }

            fileHandler.LoadFile("PlayerData", (string data) =>
            {
                PlayerData playerData;

                if (!TryToParseFromJson(data, out playerData))
                {
                    callback(null);
                    return;
                }

                callback(playerData);
            });
        });
    }

    private bool TryToParseToJson(PlayerData data, out string json)
    {
        try
        {
            json = JsonUtility.ToJson(data);

            return true;
        }
        catch (Exception exception)
        {
            if (Application.isEditor)
                throw exception;

            json = null;

            return false;
        }
    }

    private bool TryToParseFromJson(string json, out PlayerData playerData)
    {
        if (string.IsNullOrEmpty(json))
        {
            playerData = null;
            return false;
        }

        try
        {
            playerData = JsonUtility.FromJson<PlayerData>(json);
            return true;
        }
        catch(Exception exception)
        {
            if (Application.isEditor)
                throw exception;

            playerData = null;
            return false;
        }
    }
}
