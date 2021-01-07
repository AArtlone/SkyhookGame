using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmicPortLandLaunchManager
{
    public bool IsLandingOrLaunching { get; private set; }

    private Queue<CosmicPortQueueElement> shipsQueue;

    private MonoBehaviour mono;

    private ShipPrefab shipPrefab;
    private ShipPrefab spawnedShipPrefab;

    private Transform launchShipContainer;
    private Transform landShipContainer;

    public CosmicPortLandLaunchManager(MonoBehaviour mono, ShipPrefab shipPrefab, Transform launchShipContainer, Transform landShipContainer)
    {
        shipsQueue = new Queue<CosmicPortQueueElement>();

        this.mono = mono;
        this.shipPrefab = shipPrefab;
        this.launchShipContainer = launchShipContainer;
        this.landShipContainer = landShipContainer;
    }

    public void LaunchShip(Dock launchingDock, Dock destinationDock, Planet destination, int level)
    {
        if (IsLandingOrLaunching)
        {
            AddToQueue(LandOrLaunch.Launch, launchingDock, destinationDock, destination);
            return;
        }

        ShipPrefab ship = Object.Instantiate(shipPrefab, launchShipContainer);
        ship.Launch(launchingDock.Ship.shipType, level, landShipContainer.position.y);

        SetToBusy();

        TripsManager.Instance.StartNewTrip(Settlement.Instance.Planet, destination, GetTimeToDestination(destination), launchingDock.Ship, destinationDock);

        launchingDock.RemoveShip();

        mono.StartCoroutine(HandleNextQueueElementCo());
    }

    public void LandShip(Trip trip)
    {
        if (IsLandingOrLaunching)
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
        IsLandingOrLaunching = false;

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
            LaunchShip(nextElement.launchDock, nextElement.destinationDock, nextElement.destination, cosmicPortLevel);

            shipsQueue.Dequeue();
        }
    }

    public void AddToQueue(LandOrLaunch landOrLaunch, Dock launchDock, Dock destinationDock, Planet destination)
    {
        shipsQueue.Enqueue(new CosmicPortQueueElement(landOrLaunch, launchDock, destinationDock, destination));
    }

    public void AddToQueue(LandOrLaunch landOrLaunch, Trip trip)
    {
        shipsQueue.Enqueue(new CosmicPortQueueElement(landOrLaunch, trip));
    }

    public void SetToBusy()
    {
        IsLandingOrLaunching = true;
    }

    private int GetTimeToDestination(Planet destination)
    {
        return 5;
    }
}

public class CosmicPortQueueElement
{
    public LandOrLaunch landOrLaunch;
    public Dock launchDock;
    public Dock destinationDock;
    public Planet destination;
    public Trip trip;

    public CosmicPortQueueElement(LandOrLaunch landOrLaunch, Dock launchDock, Dock destinationDock, Planet destination)
    {
        this.landOrLaunch = landOrLaunch;
        this.launchDock = launchDock;
        this.destinationDock = destinationDock;
        this.destination = destination;
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
