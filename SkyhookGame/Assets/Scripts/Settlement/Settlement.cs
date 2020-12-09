using MyUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : Singleton<Settlement>, ISavable<SettlementData>
{
    [SerializeField] private ExperienceModule experienceModule = default;

    [SerializeField] private CosmicPort cosmicPort = default;
    [SerializeField] private Community community = default;
    [SerializeField] private Production production = default;
    [SerializeField] private StarLabs starLabs = default;
    [SerializeField] private Manufactory manufactory = default;

    public CosmicPort CosmicPort { get { return cosmicPort; } }
    public Community Community { get { return community; } }
    public Production Production { get { return production; } }
    public StarLabs StarLabs { get { return starLabs; } }
    public Manufactory Manufactory { get { return manufactory; } }

    public ResourcesModule ResourcesModule { get; private set; }

    protected override void Awake()
    {
        SetInstance(this);

        SceneLoader.Instance.AddSaveDataWaiter(this);
    }

    private IEnumerator Start()
    {
        yield return SceneLoader.Instance.WaitForLoading();

        var playerData = PlayerDataManager.Instance.PlayerData;

        if (playerData == null)
        {
            Debug.LogWarning($"Settlement data is null");
            SaveDataIsNull();
        }
        else
            SetSavableData(playerData.settlementData);

        SceneLoader.Instance.RemoveSaveDataWaiter(this);
    }

    private void SaveDataIsNull()
    {
        ResourcesModule = new ResourcesModule();
        ResourcesModule.resources.ForEach(r => r.IncreaseAmount(5000));
    }

    public void SetTestResources(int testAmount)
    {
        ResourcesModule.resources.ForEach(r => r.SetAmount(testAmount));
    }

    public void AddExperience(int amount)
    {
        experienceModule.Increase(amount);

        Debug.Log(experienceModule.Experience);
    }

    public SettlementData CreatSaveData()
    {
        var cosmicPortData = CosmicPort.CreatSaveData();
        var manufactoryData = Manufactory.CreatSaveData();
        var starLabsData = StarLabs.CreatSaveData();

        var resources = ResourcesModule.resources;
        
        var settlementData = new SettlementData(cosmicPortData, manufactoryData, starLabsData, resources);

        return settlementData;
    }

    public void SetSavableData(SettlementData data)
    {
        ResourcesModule = new ResourcesModule(data.resources);
    }
}

[System.Serializable]
public class SettlementData
{
    public CosmicPortData cosmicPortData;
    public ManufactoryData manufactoryData;
    public StarLabsData starLabsData;
    public List<Resource> resources;

    public SettlementData(CosmicPortData cosmicPortData, ManufactoryData manufactoryData, StarLabsData starLabsData, List<Resource> resources)
    {
        this.cosmicPortData = cosmicPortData;
        this.manufactoryData = manufactoryData;
        this.starLabsData = starLabsData;
        this.resources = resources;
    }
}
