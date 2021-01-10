using MyUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : Singleton<Settlement>, ISavable<SettlementData>
{
    [SerializeField] private Planet planet = default;

    [Space(5f)]
    [SerializeField] private ExperienceModule experienceModule = default;

    [SerializeField] private CosmicPort cosmicPort = default;
    [SerializeField] private Community community = default;
    [SerializeField] private Production production = default;
    [SerializeField] private StarLabs starLabs = default;
    [SerializeField] private Manufactory manufactory = default;

    public Planet Planet { get { return planet; } }
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
        SettlementData settlementData = null;

        if (playerData != null)
            settlementData = playerData.GetSettlementData(planet);

        if (playerData == null || settlementData == null)
        {
            Debug.LogWarning($"Settlement data is null");
            SaveDataIsNull();
        }
        else
            SetSavableData(settlementData);

        SceneLoader.Instance.RemoveSaveDataWaiter(this);
    }

    private void SaveDataIsNull()
    {
        ResourcesModule = new ResourcesModule();
        ResourcesModule.resources.ForEach(r => r.ChangeAmount(100));
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

    public void ReceiveResources(ResourcesModule resourcesToReceive)
    {
        foreach (var resource in resourcesToReceive.resources)
            ResourcesModule.IncreaseResource(resource.ResourceType, resource.Amount);
    }

    public SettlementData CreateSaveData()
    {
        var cosmicPortData = CosmicPort.CreateSaveData();
        var manufactoryData = Manufactory.CreateSaveData();
        var starLabsData = StarLabs.CreateSaveData();
		//var productionData = Production.CreateSaveData();

		var resources = ResourcesModule.resources;
        
        var settlementData = new SettlementData(planet, cosmicPortData, manufactoryData, starLabsData, resources);

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
    public Planet planet;
    public CosmicPortData cosmicPortData;
    public ManufactoryData manufactoryData;
    public StarLabsData starLabsData;
	public ProductionData productionData;
    public List<Resource> resources;

    public SettlementData(Planet planet, CosmicPortData cosmicPortData, ManufactoryData manufactoryData, StarLabsData starLabsData, List<Resource> resources)
    {
        this.planet = planet;
        this.cosmicPortData = cosmicPortData;
        this.manufactoryData = manufactoryData;
        this.starLabsData = starLabsData;
		//this.productionData = productionData;
        this.resources = resources;
    }

    public SettlementData(SettlementData oldSettlementData, List<Resource> newResources)
    {
        planet = oldSettlementData.planet;
        cosmicPortData = oldSettlementData.cosmicPortData;
        manufactoryData = oldSettlementData.manufactoryData;
        starLabsData = oldSettlementData.starLabsData;
        resources = newResources;
    }
}
