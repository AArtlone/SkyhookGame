using MyUtilities.GUI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendShipViewController : ViewController
{
    [Space(10f)]
    [SerializeField] private TextMeshProUGUI shipNameText = default;
    [SerializeField] private TextMeshProUGUI shipMassText = default;
    [SerializeField] private TextMeshProUGUI reqFuelText = default;

    [SerializeField] private RectTransform adjustersContainer = default;

    [SerializeField] private ResourceAdjuster resourceAdjusterPrefab = default;

    [SerializeField] private MyButton sendButton = default;

    private List<ResourceAdjuster> resourceAdjusters;

    private Dock dock;

    private const string MassText = "Mass: ";
    private const string ReqFuelText = "Req Fuel: ";

    public override void ViewWillBeFocused()
    {
        base.ViewWillBeFocused();

        shipNameText.text = dock.Ship.shipName;
        shipMassText.text = MassText + dock.Ship.shipMass.ToString();

        reqFuelText.text = ReqFuelText + CalculateReqFuel().ToString();

        sendButton.SetInteractable(false);
    }

    private void ResourceAdjuster_OnResourceChange()
    {
        shipMassText.text = MassText + CalculateTotalMass().ToString();
        reqFuelText.text = ReqFuelText + CalculateReqFuel().ToString();

        sendButton.SetInteractable(CanSend());
    }

    public void AssignDock(Dock dock)
    {
        this.dock = dock;

        dock.Ship.resourcesModule = new ResourcesModule();

        DestroyResourceAdjusters();

        CreateResourceAdjusters();
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

        resourceAdjuster.SetUpAdjuster(resource.ResourceType);

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

    private int CalculateTotalMass()
    {
        int result = dock.Ship.shipMass;

        foreach (var v in resourceAdjusters)
            result += GetResourceTotalWeight(v.Amount, v.ResourceType);

        return result;
    }

    private int CalculateReqFuel()
    {
        int shipMass = dock.Ship.shipMass;
        int totalCargoWeight = 0;

        foreach (var v in resourceAdjusters)
            totalCargoWeight += GetResourceTotalWeight(v.Amount, v.ResourceType);

        float reqFuel = GetReqFuel(shipMass + totalCargoWeight);

        return (int)reqFuel;
    }

    private float GetReqFuel(int totalWeight)
    {
        return .25f * totalWeight;
    }

    private int GetResourceTotalWeight(int amount, ResourcesDSID resourceType)
    {
        int massOfOneUnit = DSModelManager.Instance.ResourcesModel.GetOneUnitMass(resourceType);

        return amount * massOfOneUnit;
    }

    public void Btn_Send()
    {

    }

    private bool CanSend()
    {
        // Check if destination has empty dock

        // Check if has enough fuel
        bool canSend;
        canSend = GetCurrentFuelAmount() >= CalculateReqFuel();

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
