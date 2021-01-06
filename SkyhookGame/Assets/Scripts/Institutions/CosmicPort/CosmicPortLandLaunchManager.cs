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

    public void LaunchShip(Dock dock, Planet destination, int level)
    {
        if (IsLandingOrLaunching)
        {
            AddToQueue(LandOrLaunch.Launch, dock, destination);
            return;
        }

        ShipPrefab ship = Object.Instantiate(shipPrefab, launchShipContainer);
        ship.Launch(dock.Ship.shipType, level, landShipContainer.position.y);

        SetToBusy();

        TripsManager.Instance.StartNewTrip(Settlement.Instance.Planet, destination, GetTimeToDestination(destination), dock.Ship);

        dock.RemoveShip();

        mono.StartCoroutine(HandleNextQueueElementCo());
    }

    public void LandShip(Ship shipToLand)
    {
        if (IsLandingOrLaunching)
        {
            AddToQueue(LandOrLaunch.Land, shipToLand);
            return;
        }

        spawnedShipPrefab = Object.Instantiate(shipPrefab, landShipContainer);
        spawnedShipPrefab.Land(shipToLand, launchShipContainer.position.y);
        spawnedShipPrefab.onLanded += OnLanded;

        SetToBusy();

        mono.StartCoroutine(HandleNextQueueElementCo());
    }
    private void OnLanded(Ship ship)
    {
        spawnedShipPrefab.onLanded -= OnLanded;

        if (ship.resourcesModule == null)
            return;

        Settlement.Instance.ReceiveResources(ship.resourcesModule);
    }

    private IEnumerator HandleNextQueueElementCo()
    {
        yield return new WaitForSeconds(3.5f);

        HandleNextQueueElement();
    }

    public void HandleNextQueueElement()
    {
        IsLandingOrLaunching = false;

        if (shipsQueue.Count == 0)
            return;

        var nextElement = shipsQueue.Peek();

        if (nextElement == null)
            return;

        if (nextElement.landOrLaunch == LandOrLaunch.Land)
        {
            LandShip(nextElement.ship);
            shipsQueue.Dequeue();
        }
        else
        {
            int cosmicPortLevel = Settlement.Instance.CosmicPort.LevelModule.Level;
            LaunchShip(nextElement.dock, nextElement.destination, cosmicPortLevel);

            shipsQueue.Dequeue();
        }
    }

    public void AddToQueue(LandOrLaunch landOrLaunch, Dock dock, Planet destination)
    {
        shipsQueue.Enqueue(new CosmicPortQueueElement(landOrLaunch, dock, destination));
    }

    public void AddToQueue(LandOrLaunch landOrLaunch, Ship ship)
    {
        shipsQueue.Enqueue(new CosmicPortQueueElement(landOrLaunch, ship));
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
    public Dock dock;
    public Planet destination;
    public Ship ship;

    public CosmicPortQueueElement(LandOrLaunch landOrLaunch, Dock dock, Planet destination)
    {
        this.landOrLaunch = landOrLaunch;
        this.dock = dock;
        this.destination = destination;
    }

    public CosmicPortQueueElement(LandOrLaunch landOrLaunch, Ship ship)
    {
        this.landOrLaunch = landOrLaunch;
        this.ship = ship;
    }
}

public enum LandOrLaunch
{
    Land, Launch
}
