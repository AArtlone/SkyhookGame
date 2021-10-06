using System.Collections.Generic;
using UnityEngine;

public class CosmicPortSkyhookLandLaunchManager
{
    private Queue<CosmicPortQueueElement> shipsQueue;

    private ShipInSkyhoookPrefab shipPrefab;

    public CosmicPortSkyhookLandLaunchManager(ShipInSkyhoookPrefab shipPrefab)
    {
        shipsQueue = new Queue<CosmicPortQueueElement>();

        this.shipPrefab = shipPrefab;
    }

    public void TryToLaunchShip(SendShipData sendShipData)
    {
        var container = GetContainerToSpawn();

        if (container == null)
        {
            AddToQueue(LandOrLaunch.Launch, sendShipData);
            return;
        }

        container.SpawnShipForLaunch(shipPrefab, sendShipData);
    }

    public void AddToQueue(LandOrLaunch landOrLaunch, SendShipData sendShipData)
    {
        shipsQueue.Enqueue(new CosmicPortQueueElement(landOrLaunch, sendShipData));
    }

    public void AddToQueue(LandOrLaunch landOrLaunch, Trip trip)
    {
        shipsQueue.Enqueue(new CosmicPortQueueElement(landOrLaunch, trip));
    }

    private SkyhookContainer GetContainerToSpawn()
    {
        foreach (var container in SkyhookManager.Instance.ContainersWithSkyhooks)
        {
            bool canLaunch = container.CanSpawnShipForLaunch();

            Debug.Log("CanLaunch:" + canLaunch);

            if (canLaunch)
                return container;
        }

        return null;
    }
}
