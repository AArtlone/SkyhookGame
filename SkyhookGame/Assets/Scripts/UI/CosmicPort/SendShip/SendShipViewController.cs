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

    private List<ResourceAdjuster> resourceAdjusters;

    private ResourcesModule resourcesModule;

    private Dock dock;

    public override void ViewWillBeFocused()
    {
        base.ViewWillBeFocused();

        shipNameText.text = dock.Ship.shipName;
        shipMassText.text = dock.Ship.shipMass.ToString();

        reqFuelText.text = CalculateReqFuel().ToString();
    }

    private void ResourceAdjuster_OnResourceChange()
    {
        shipMassText.text = CalculateTotalMass().ToString();
        reqFuelText.text = CalculateReqFuel().ToString();
    }

    public void AssignDock(Dock dock)
    {
        this.dock = dock;

        resourcesModule = new ResourcesModule();

        DestroyResourceAdjusters();

        CreateResourceAdjusters();
    }

    private void CreateResourceAdjusters()
    {
        resourceAdjusters = new List<ResourceAdjuster>(resourcesModule.resources.Count);
        resourcesModule.resources.ForEach(r => CreateResourceAdjuster(r));

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

        return GetReqFuel(shipMass + totalCargoWeight);
    }

    private int GetReqFuel(int totalWeight)
    {
        return 10 * totalWeight;
    }

    private int GetResourceTotalWeight(int amount, ResourcesDSID resourceType)
    {
        int massOfOneUnit = DSModelManager.Instance.ResourcesModel.GetOneUnitMass(resourceType);

        return amount * massOfOneUnit;
    }
}
