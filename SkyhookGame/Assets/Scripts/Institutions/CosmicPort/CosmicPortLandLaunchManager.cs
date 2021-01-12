using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmicPortLandLaunchManager
{
    private Queue<CosmicPortQueueElement> shipsQueue;

    private MonoBehaviour mono;

    private ShipPrefab shipPrefab;
    private ShipPrefab spawnedShipPrefab;

    private Transform launchShipContainer;
    private Transform landShipContainer;

    private bool isLandingOrLaunching;

    public CosmicPortLandLaunchManager(MonoBehaviour mono, ShipPrefab shipPrefab, Transform launchShipContainer, Transform landShipContainer)
    {
        shipsQueue = new Queue<CosmicPortQueueElement>();

        this.mono = mono;
        this.shipPrefab = shipPrefab;
        this.launchShipContainer = launchShipContainer;
        this.landShipContainer = landShipContainer;
    }

    public void LaunchShip(SendShipData sendShipData, int level)
    {
        if (isLandingOrLaunching)
        {
            AddToQueue(LandOrLaunch.Launch, sendShipData);
            return;
        }

        ShipPrefab ship = Object.Instantiate(shipPrefab, launchShipContainer);
        ship.Launch(sendShipData.launchingDock.Ship.shipType, level, landShipContainer.position.y);

        SetToBusy();

        TripsManager.Instance.StartNewTrip(Settlement.Instance.Planet, GetTimeToDestination(sendShipData.destination), sendShipData.launchingDock.Ship, sendShipData.destinationDock);

        sendShipData.launchingDock.RemoveShip();

        mono.StartCoroutine(HandleNextQueueElementCo());
    }

    public void LandShip(Trip trip)
    {
        if (isLandingOrLaunching)
        {
            AddToQueue(LandOrLaunch.Land, trip);
            return;
        }

        spawnedShipPrefab = Object.Instantiate(shipPrefab, landShipContainer);
        spawnedShipPrefab.Land(trip, launchShipContainer.position.y);
        spawnedShipPrefab.onLanded += OnLanded;

        SetToBusy();

        mono.StartCoroutine(HandleNextQueueElementCo());
    }

    private void OnLanded(Trip trip)
    {
        spawnedShipPrefab.onLanded -= OnLanded;

        foreach (var dock in Settlement.Instance.CosmicPort.AllDocks)
        {
            if (dock.DockID != trip.destinationDock.DockID)
                continue;

            dock.ReceiveShip(trip.ship);
        }


        if (trip.ship.resourcesModule == null)
            return;

        Settlement.Instance.ReceiveResources(trip.ship.resourcesModule);

        trip.ship.resourcesModule = null;
    }

    private IEnumerator HandleNextQueueElementCo()
    {
        yield return new WaitForSeconds(3.5f);

        HandleNextQueueElement();
    }

    private void HandleNextQueueElement()
    {
        isLandingOrLaunching = false;

        if (shipsQueue.Count == 0)
            return;

        var nextElement = shipsQueue.Peek();

        if (nextElement == null)
            return;

        if (nextElement.landOrLaunch == LandOrLaunch.Land)
        {
            LandShip(nextElement.trip);
            shipsQueue.Dequeue();
        }
        else
        {
            int cosmicPortLevel = Settlement.Instance.CosmicPort.LevelModule.Level;
            LaunchShip(nextElement.sendShipData, cosmicPortLevel);

            shipsQueue.Dequeue();
        }
    }

    public void AddToQueue(LandOrLaunch landOrLaunch, SendShipData sendShipData)
    {
        shipsQueue.Enqueue(new CosmicPortQueueElement(landOrLaunch, sendShipData));
    }

    public void AddToQueue(LandOrLaunch landOrLaunch, Trip trip)
    {
        shipsQueue.Enqueue(new CosmicPortQueueElement(landOrLaunch, trip));
    }

    public void SetToBusy()
    {
        isLandingOrLaunching = true;
    }

    private int GetTimeToDestination(Planet destination)
    {
        return 5;
    }
}

public class CosmicPortQueueElement
{
    public LandOrLaunch landOrLaunch;
    public SendShipData sendShipData;
    public Trip trip;

    public CosmicPortQueueElement(LandOrLaunch landOrLaunch, SendShipData sendShipData)
    {
        this.landOrLaunch = landOrLaunch;
        this.sendShipData = sendShipData;
    }

    public CosmicPortQueueElement(LandOrLaunch landOrLaunch, Trip trip)
    {
        this.landOrLaunch = landOrLaunch;
        this.trip = trip;
    }
}

public enum LandOrLaunch
{
    Land, Launch
}
