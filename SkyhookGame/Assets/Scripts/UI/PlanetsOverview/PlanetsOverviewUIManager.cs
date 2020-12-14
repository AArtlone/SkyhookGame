using MyUtilities;
using MyUtilities.GUI;
using UnityEngine;

public class PlanetsOverviewUIManager : Singleton<PlanetsOverviewUIManager>
{
    [Header("Navigation Controllers")]
    [SerializeField] private NavigationController navigationController = default;

    [Header("View Controllers")]
    [SerializeField] private PlanetsOverviewViewController planetsOverview = default;

    public PlanetsOverviewViewController PlanetsOverviewViewController { get { return planetsOverview; } }

    protected override void Awake()
    {
        SetInstance(this);
    }

    public void ShowSolarSystemView()
    {
        navigationController.Push(planetsOverview);
    }
}
