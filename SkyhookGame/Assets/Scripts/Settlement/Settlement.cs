using MyUtilities;
using System.Collections;
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

    public SettlementData GetSavableData()
    {
        var cosmicPortData = CosmicPort.GetSavableData();
        var settlementData = new SettlementData(cosmicPortData);
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

    public SettlementData(CosmicPortData cosmicPortData)
    {
        this.cosmicPortData = cosmicPortData;
    }
}
