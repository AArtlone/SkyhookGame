﻿using System.Collections.Generic;

public class SendShipManager
{
    private List<ResourceAdjuster> resourceAdjusters;

    private Ship ship;

    public SendShipManager(Ship ship, List<ResourceAdjuster> resourceAdjusters)
    {
        this.ship = ship;
        this.resourceAdjusters = resourceAdjusters;
    }

    public int CalculateTotalMass()
    {
        int result = ship.shipMass;

        foreach (var v in resourceAdjusters)
            result += GetResourceTotalWeight(v.Amount, v.ResourceType);

        return result;
    }

    public int CalculateReqFuel()
    {
        int shipMass = ship.shipMass;
        int totalCargoWeight = 0;

        foreach (var v in resourceAdjusters)
            totalCargoWeight += GetResourceTotalWeight(v.Amount, v.ResourceType);

        float reqFuel = GetReqFuel(shipMass + totalCargoWeight);

        return (int)reqFuel;
    }

    public float GetReqFuel(int totalWeight)
    {
        return .25f * totalWeight;
    }

    public int GetResourceTotalWeight(int amount, ResourcesDSID resourceType)
    {
        int massOfOneUnit = DSModelManager.Instance.ResourcesModel.GetOneUnitMass(resourceType);

        return amount * massOfOneUnit;
    }

    public bool CanLaunch(int currentFuelAmount)
    {
        // Check if has enough fuel
        bool canSend;
        canSend = currentFuelAmount >= CalculateReqFuel();

        return canSend;
    }
}
