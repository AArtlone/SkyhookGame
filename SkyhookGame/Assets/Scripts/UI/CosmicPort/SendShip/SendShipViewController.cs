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

    [SerializeField] private RectTransform adjustersContainer = default;

    [SerializeField] private ResourceAdjuster resourceAdjusterPrefab = default;

    [SerializeField] private MyButton sendButton = default;


    private SendShipManager sendShipManager;

    private Dock dock;
    private List<ResourceAdjuster> resourceAdjusters;

    public override void ViewWillBeFocused()
    {
        base.ViewWillBeFocused();

        shipNameText.text = dock.Ship.shipName;
        shipMassText.text = MassText + dock.Ship.shipMass.ToString();

        reqFuelText.text = ReqFuelText + sendShipManager.CalculateReqFuel().ToString();

        sendButton.SetInteractable(CanSend());
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

    private void ResourceAdjuster_OnResourceChange()
    {
        shipMassText.text = MassText + sendShipManager.CalculateTotalMass().ToString();
        reqFuelText.text = ReqFuelText + sendShipManager.CalculateReqFuel().ToString();

        sendButton.SetInteractable(CanSend());
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
        CosmicPortGUIManager.Instance.Back();
        Settlement.Instance.CosmicPort.SendShip(dock, "Moon");
    }

    private bool CanSend()
    {
        // Check if destination has empty dock

        // Check if has enough fuel
        bool canSend;
        canSend = GetCurrentFuelAmount() >= sendShipManager.CalculateReqFuel();

        return canSend;
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
