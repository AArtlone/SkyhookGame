using MyUtilities.GUI;
using System.Collections.Generic;
using UnityEngine;

public class SkyhookContainer : BaseTouchController
{
    private const string LayerName = "SkyhookContainer";
    [SerializeField] private List<GameObject> blockingObjects = default; // A list of UI objects that should block raycast

    [Space(5f)]
    [SerializeField] private bool facingRightSideOfTheScreen = default;

    [Space(5f)]
    [SerializeField] private Skyhook skyhook = default;
    [SerializeField] private GameObject blueprint = default;
    
    [Space(5f)]
    [SerializeField] private Vector3 startTrackingVector = default;
    [SerializeField] private Vector3 endTrackingVector = default;
    [SerializeField] private Vector3 launchPointVector = default;
    [SerializeField] private Vector3 landingPointVector = default;

    public GameObject Blueprint { get { return blueprint; } }
    public Vector3 StartTrackingVector { get { return startTrackingVector;} }
    public Vector3 EndTrackingVector { get { return endTrackingVector; } }
    public Vector3 LaunchPointVector { get { return launchPointVector; } }
    public Vector3 LandingPointVector { get { return landingPointVector; } }

    public bool FacingRightSideOfTheScreen { get { return facingRightSideOfTheScreen; } }

    private LayerMask layerMask = default;

    private ShipInSkyhoookPrefab shipPrefab;

    private bool hasShipAssigned;

    private SendShipData sendShipData;

    protected override void Awake()
    {
        if (blueprint == null)
        {
            Debug.LogWarning($"Preview is not assigned in the editor on {gameObject.name} gameobject");
            enabled = false;
        }

        base.Awake();

        layerMask = 1 << LayerMask.NameToLayer(LayerName);
    }

    public void EnableSkyhook()
    {
        base.DisableInput();

        blueprint.SetActive(false);

        skyhook.Initialize(this);
    }

    protected override void Update()
    {
        if (IsInputDisabled) return;

        if (InstitutionsUIManager.Instance.IsUIDisplayed) return;

        foreach (var obj in blockingObjects)
            if (obj.activeSelf)
                return;

        if (Application.isEditor)
        {
            HandleEditorInput();
            return;
        }

        HandleTouchInput();
    }

    protected override LayerMask GetLayerMask()
    {
        return layerMask;
    }

    protected override int GetLayerIndex()
    {
        return LayerMask.NameToLayer(LayerName);
    }

    protected override void OnTouch()
    {
        int skyhooksInStorage = Settlement.Instance.Manufactory.SkyhooksInStorage;

        if (skyhooksInStorage == 0)
            ShowNoSkyhooksPopUp();
        else
            SkyhookManager.Instance.SpawnSkyhook(this);
    }

    private void ShowNoSkyhooksPopUp()
    {
        var description = "There are no Skyhooks in the storage";

        var buttonText = "Ok";

        PopUpManager.CreateSingleButtonTextPopUp(description, buttonText);
    }

    public void SpawnShipForLaunch(ShipInSkyhoookPrefab shipPrefab, SendShipData sendShipData)
    {
        this.sendShipData = sendShipData;

        this.shipPrefab = Instantiate(shipPrefab, skyhook.ShipContainer);

        this.shipPrefab.AssignShipSrite(sendShipData.launchingDock.Ship.shipType);

        skyhook.onReachedLaunchPoint += Skyhook_OnReachedLaunchPoint;

        hasShipAssigned = true;
    }

    private void Skyhook_OnReachedLaunchPoint()
    {
        skyhook.onReachedLaunchPoint -= Skyhook_OnReachedLaunchPoint;

        Planet currentPlanet = Settlement.Instance.Planet;
        TripsManager.Instance.StartNewTrip(currentPlanet, sendShipData.destination, GetTimeToDestination(sendShipData.destination), sendShipData.launchingDock.Ship, sendShipData.destinationDock);

        sendShipData.launchingDock.RemoveShip();

        if (shipPrefab == null)
            return;

        Destroy(this.shipPrefab.gameObject);
    }

    public void SpawnShipForLanding(ShipInSkyhoookPrefab shipPrefab, Trip trip)
    {
        var ship = Instantiate(shipPrefab, skyhook.ShipContainer);

        ship.WaitForLanding(skyhook, trip);
    }

    public bool CanSpawnShipForLaunch()
    {
        bool connectionPointIsOnScreen = !skyhook.IsNotOnScreen;

        return connectionPointIsOnScreen && !hasShipAssigned;
    }

    public bool CanSpawnShipForLanding()
    {
        bool connectionPointIsOffScreen = skyhook.IsNotOnScreen;

        return connectionPointIsOffScreen && !hasShipAssigned;
    }

    public bool CanLaunch()
    {
        return false;
    }

    private int GetTimeToDestination(Planet destination)
    {
        return 8;
    }
}