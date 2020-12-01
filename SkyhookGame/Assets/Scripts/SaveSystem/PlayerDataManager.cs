using MyUtilities;
using System.Collections;
using UnityEngine;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    public PlayerData PlayerData { get; private set; }

    private bool doneLoadingData;

    protected override void Awake()
    {
        SetInstance(this);

        Application.quitting += Application_quitting;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        LoadData();
    }

    private void Application_quitting()
    {
        var data = new PlayerData(Settlement.Instance.CosmicPort.GetSavableData());

        print(data.docksData);

        SaveData(data);
    }

    public WaitUntil WaitForPlayerDataLoad()
    {
        return new WaitUntil(() =>
        {
            return doneLoadingData;
        });
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
                doneLoadingData = true;
                return;
            }

            PlayerData = result;
            doneLoadingData = true;
        });

        IOUtility<PlayerData>.LoadData("PlayerData", callback);
    }
}