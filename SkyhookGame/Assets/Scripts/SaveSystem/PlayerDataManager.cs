using MyUtilities;
using System;
using UnityEngine;

public class PlayerDataManager : PersistentSingleton<PlayerDataManager>
{
    public PlayerData PlayerData { get; private set; }

    protected override void Awake()
    {
        SetInstance(this);

        if (WillBeDestroyed)
            return;

        Application.quitting += Application_quitting;

        SceneLoader.Instance.AddWaiter(this);

        LoadData();
    }

    private void Application_quitting()
    {
        SaveSettlementData(Settlement.Instance.Planet, new Action(() => { SaveData(PlayerData); }));
    }

    public void SaveSettlementData(Planet planet, Action callback = null)
    {
        if (PlayerData == null)
            PlayerData = new PlayerData();

        PlayerData.SaveSettlementData(planet, Settlement.Instance.CreatSaveData());

        callback?.Invoke();
    }

    private void SaveData(PlayerData data)
    {
        Action<bool> callback = new Action<bool>((b) => 
        { 
            if (!b)
                Debug.LogWarning("Failed to save PlayerData");
        });

        IOUtility<PlayerData>.SaveData(data, "PlayerData", callback);
    }

    private void LoadData()
    {
        Action<PlayerData> callback = new Action<PlayerData>((result) =>
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