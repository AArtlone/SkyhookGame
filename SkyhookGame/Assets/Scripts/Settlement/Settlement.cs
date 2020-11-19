using MyUtilities;
using UnityEngine;

public class Settlement : Singleton<Settlement>
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

    //public string Name { get; private set; }

    protected override void Awake()
    {
        SetInstance(this);
    }

    public void AddExperience(int amount)
    {
        experienceModule.Increase(amount);

        Debug.Log(experienceModule.Experience);
    }
}
