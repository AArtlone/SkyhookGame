using MyUtilities.GUI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SendShipViewController : ViewController
{
    private const string CurrentNonFuelText = "Current non fuel: ";
    private const string MaxNonFuelMassText = "Max non fuel mass: ";
    private const string CurrentFuelText = "Current fuel: ";
    private const string ReqFuelText = "Required Fuel: ";

    [SerializeField] private CosmicPortUIManager cosmicPortUIManager = default;

    [Space(10f)]
    [SerializeField] private TextMeshProUGUI shipNameText = default;
    [SerializeField] private TextMeshProUGUI currentNonFuel = default;
    [SerializeField] private TextMeshProUGUI maxNonFuelMassText = default;
    [SerializeField] private TextMeshProUGUI currentFuel = default;
    [SerializeField] private TextMeshProUGUI reqFuelText = default;

    [Space(5f)]
    [SerializeField] private Image shipImage = default;

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

        SetShipVisuals();

        sendButton.SetInteractable(sendShipManager.CanLaunch(GetCurrentFuelAmount(), selectedLaunchMethod == LaunchMethod.Skyhook, dock.Ship.shipType));

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

    private void SetShipVisuals()
    {
        var shipSprite = Resources.Load<Sprite>($"Sprites/Ships/{dock.Ship.shipType}");
        shipImage.sprite = shipSprite;

        shipNameText.text = dock.Ship.shipName;
        currentNonFuel.text = CurrentNonFuelText + 0.ToString();
        currentFuel.text = CurrentFuelText + 0;
        
        int maxNonFuel = dock.Ship.shipMass / 10;
        maxNonFuelMassText.text = MaxNonFuelMassText + (maxNonFuel).ToString();

        SetReqFuel();
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
            SetReqFuel();
        }
    }

    private void ResourceAdjuster_OnResourceChange()
    {
        currentNonFuel.text = CurrentNonFuelText + sendShipManager.GetCurrentNonFuel().ToString();

        currentFuel.text = CurrentFuelText + sendShipManager.GetCurrentFuel().ToString();

        sendButton.SetInteractable(sendShipManager.CanLaunch(GetCurrentFuelAmount(), selectedLaunchMethod == LaunchMethod.Skyhook, dock.Ship.shipType));
    }

    private void SetReqFuel()
    {
        bool viaSkyhook = selectedLaunchMethod == LaunchMethod.Skyhook;
        ShipsDSID shipID = dock.Ship.shipType;
        reqFuelText.text = ReqFuelText + DSModelManager.Instance.ShipsModel.GetReqFuel(shipID, viaSkyhook);
    }

    private void CreateResourceAdjusters()
    {
        resourceAdjusters = new List<ResourceAdjuster>(dock.Ship.resourcesModule.resources.Count);
        dock.Ship.resourcesModule.resources.ForEach(r => CreateResourceAdjuster(r));

        //if (dock.Ship.shipType.Equals(ShipsDSID.Craft))
        //{
        //    // ADD humans adjuster
        //}
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
                //dock.Ship.resourcesModule.IncreaseResource(ResourcesDSID.Fuel, -sendShipManager.CalculateReqFuel(selectedLaunchMethod == LaunchMethod.Skyhook));

                InstitutionsUIManager.Instance.CosmicPortUIManager.Back();

                var sendShipData = new SendShipData(dock, destinationDock, selectedDestination);
                Settlement.Instance.CosmicPort.LaunchShip(sendShipData, selectedLaunchMethod);

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

    public void Btn_ClearDock()
    {

        string text = $"Do you really want to clear the dock and destroy the ship?";
        string button1Text = "Yes";
        string button2Text = "No";
        Action button1Callback = new Action(() =>
        {
            dock.RemoveShip();

            cosmicPortUIManager.Back();
        });

        PopUpManager.CreateDoubleButtonTextPopUp(text, button1Text, button2Text, button1Callback);
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
        //TODO: remove return when done testing
        return;

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
