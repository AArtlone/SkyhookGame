using MyUtilities;
using UnityEngine;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    public PlayerData PlayerData { get; private set; }

    protected override void Awake()
    {
        SetInstance(this);

        print("Setting instance");

        Application.quitting += Application_quitting;

        SceneLoader.Instance.AddWaiter(this);

        LoadData();
    }

    private void Application_quitting()
    {
        var data = new PlayerData(Settlement.Instance.CosmicPort.GetSavableData());

        SaveData(data);
    }

    public void SaveData(PlayerData data)
    {
        System.Action<bool> callback = new System.Action<bool>((b) => 
        { 
            if (!b)
                Debug.LogWarning("Failed to save PlayerData");
        });

        IOUtility<PlayerData>.SaveData(data, "PlayerData", callback);
    }

    public void LoadData()
    {
        System.Action<PlayerData> callback = new System.Action<PlayerData>((result) =>
        {
            if (result == null)
            {
                Debug.LogWarning("PlayerData is null");
                SceneLoader.Instance.RemoveWaiter(this);
                return;
            }

            PlayerData = result;
            SceneLoader.Instance.RemoveWaiter(this);
        });

        IOUtility<PlayerData>.LoadData("PlayerData", callback);
    }
}