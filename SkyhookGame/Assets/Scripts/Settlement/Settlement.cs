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
    }

    private IEnumerator Start()
    {
        yield return SceneLoader.Instance.WaitForLoading();

        ResourcesModule = new ResourcesModule();

        ResourcesModule.resources.ForEach(r => r.IncreaseAmount(50));
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
        var resources = ResourcesModule.resources;
        foreach (var v in resources)
            print(v.Amount + " | " + v.ResourceType);
        var settlementData = new SettlementData(cosmicPortData, manufactoryData, resources);
        return settlementData;
    }

    public void SetSavableData(SettlementData data)
    {
        CosmicPort.SetSavableData(data.cosmicPortData);
    }
}

[System.Serializable]
public class SettlementData
{
    public CosmicPortData cosmicPortData;
    public ManufactoryData manufactoryData;
    public List<Resource> resources;

    public SettlementData(CosmicPortData cosmicPortData, ManufactoryData manufactoryData, List<Resource> resources)
    {
        this.cosmicPortData = cosmicPortData;
        this.manufactoryData = manufactoryData;
        this.resources = resources;
    }
}
