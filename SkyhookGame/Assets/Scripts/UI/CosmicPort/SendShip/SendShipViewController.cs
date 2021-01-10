using MyUtilities.GUI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendShipViewController : ViewController
{
    private const string MassText = "Mass: ";
    private const string ReqFuelText = "Req Fuel: ";

    [Space(10f)]
    [SerializeField] private TextMeshProUGUI shipNameText = default;
    [SerializeField] private TextMeshProUGUI shipMassText = default;
    [SerializeField] private TextMeshProUGUI reqFuelText = default;

    [Space(10f)]
    [SerializeField] private RectTransform adjustersContainer = default;
    [SerializeField] private ResourceAdjuster resourceAdjusterPrefab = default;

    [Space(10f)]
    [SerializeField] private MyButton sendButton = default;
    [SerializeField] private DestinationTabGroup destinationTabGroup = default;
    [SerializeField] private LaunchMethodTabGroup launchMethodTabGroup = default;

    private SendShipManager sendShipManager;

    private Dock dock;
    private List<ResourceAdjuster> resourceAdjusters;

    private Planet selectedDestination;
    private LaunchMethod selectedLaunchMethod;

    public override void ViewWillBeFocused()
    {
        base.ViewWillBeFocused();

        shipNameText.text = dock.Ship.shipName;
        shipMassText.text = MassText + dock.Ship.shipMass.ToString();

        reqFuelText.text = ReqFuelText + sendShipManager.CalculateReqFuel().ToString();

        sendButton.SetInteractable(sendShipManager.CanLaunch(GetCurrentFuelAmount()));

        destinationTabGroup.onDestinationChanged += TabGroup_OnDestinationChanged;
        launchMethodTabGroup.onLaunchTypeChanged += LaunchTabGroup_OnMethodChanged;

        if (!destinationTabGroup.IsInitialized)
            destinationTabGroup.Initialize();

        if (!launchMethodTabGroup.IsInitialized)
            launchMethodTabGroup.Initialize();

        launchMethodTabGroup.ToggleSkyhookButton();

        if (dock.Destination != Settlement.Instance.Planet)
            destinationTabGroup.SelectDestination(dock.Destination);
        else
            destinationTabGroup.SelectFirstDestination();
    }

    public override void ViewWillBeUnfocused()
    {
        base.ViewWillBeUnfocused();

        destinationTabGroup.onDestinationChanged -= TabGroup_OnDestinationChanged;
    }

    public void AssignDock(Dock dock)
    {
        this.dock = dock;

        if (dock.Ship.resourcesModule == null)
            dock.Ship.resourcesModule = new ResourcesModule();

        DestroyResourceAdjusters();

        CreateResourceAdjusters();

        sendShipManager = new SendShipManager(dock.Ship, resourceAdjusters);
    }

    private void TabGroup_OnDestinationChanged(Planet newDestination)
    {
        if (selectedDestination != newDestination)
        {
            selectedDestination = newDestination;
            dock.SetDestination(newDestination);
        }
    }

    private void LaunchTabGroup_OnMethodChanged(LaunchMethod newLaunchMethod)
    {
        if (selectedLaunchMethod != newLaunchMethod)
        {
            selectedLaunchMethod = newLaunchMethod;
        }
    }

    private void ResourceAdjuster_OnResourceChange()
    {
        shipMassText.text = MassText + sendShipManager.CalculateTotalMass().ToString();
        reqFuelText.text = ReqFuelText + sendShipManager.CalculateReqFuel().ToString();

        sendButton.SetInteractable(sendShipManager.CanLaunch(GetCurrentFuelAmount()));
    }

    private void CreateResourceAdjusters()
    {
        resourceAdjusters = new List<ResourceAdjuster>(dock.Ship.resourcesModule.resources.Count);
        dock.Ship.resourcesModule.resources.ForEach(r => CreateResourceAdjuster(r));

        if (dock.Ship.shipType.Equals(ShipsDSID.Craft))
        {
            // ADD humans adjuster
        }
    }

    private void CreateResourceAdjuster(Resource resource)
    {
        if (resourceAdjusterPrefab == null)
        {
            Debug.LogWarning("ResourcesAdjusterPrefab is null");
            return;
        }

        ResourceAdjuster resourceAdjuster = Instantiate(resourceAdjusterPrefab, adjustersContainer);
        resourceAdjuster.SetUpAdjuster(resource);
        resourceAdjuster.onResourceChange += ResourceAdjuster_OnResourceChange;

        resourceAdjusters.Add(resourceAdjuster);
    }

    private void DestroyResourceAdjusters()
    {
        if (resourceAdjusters == null)
            return;

        resourceAdjusters.ForEach(r => Destroy(r.gameObject));

        resourceAdjusters = null;
    }

    public void Btn_Send()
    {
        List<Dock> destinationDocks = PlayerDataManager.Instance.GetDocksByPlanet(selectedDestination);

        foreach (var destinationDock in destinationDocks)
        {
            if (destinationDock.DockState == DockState.Empty)
            {
                // We need to substract the required fuel from the total fuel
                dock.Ship.resourcesModule.IncreaseResource(ResourcesDSID.Fuel, -sendShipManager.CalculateReqFuel());

                InstitutionsUIManager.Instance.CosmicPortUIManager.Back();

                Settlement.Instance.CosmicPort.LaunchShip(dock, destinationDock, selectedDestination, selectedLaunchMethod);

                // reserve this dock
                destinationDock.UpdateState(DockState.Reserved);
                ReserveDock(destinationDocks);

                return;
            }
        }

        string text = "No docks can receive the ship";
        PopUpManager.CreateSingleButtonTextPopUp(text, "Ok");
        return;
    }

    private Dock GetEmptyDestinationDock()
    {
        List<Dock> destinationDocks = PlayerDataManager.Instance.GetDocksByPlanet(selectedDestination);

        foreach (var dock in destinationDocks)
        {
            if (dock.DockState == DockState.Empty)
                return dock;
        }

        return null;
    }

    private void ReserveDock(List<Dock> docksToSave)
    {
        int cosmicPortLevel = Settlement.Instance.CosmicPort.LevelModule.Level;
        var newDocksData = new List<DockData>(docksToSave.Count);
        docksToSave.ForEach(d => newDocksData.Add(new DockData(d)));

        var cosmicPortData = new CosmicPortData(cosmicPortLevel, newDocksData);

        PlayerDataManager.Instance.PlayerData.SaveCosmicPortData(selectedDestination, cosmicPortData);
    }

    private int GetCurrentFuelAmount()
    {
        foreach (var r in resourceAdjusters)
        {
            if (r.ResourceType != ResourcesDSID.Fuel)
                continue;

            return r.Amount;
        }

        return 1;
    }
}

public class SomeManager : MyUtilities.PersistentSingleton<SomeManager>
{
    private Dictionary<Planet, List<Dock>> allDocks;

    protected override void Awake()
    {
        SetInstance(this);
    }
}
