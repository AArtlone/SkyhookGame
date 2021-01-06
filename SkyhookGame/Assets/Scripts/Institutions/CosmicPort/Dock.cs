using System;
using System.Collections.Generic;
using UnityEngine;

public class Dock
{
    public Action<DockState> onStateChange;

    public string dockName;

    [SerializeField] private DockState dockState;
    public DockState DockState { get { return dockState; } }

    public DockID DockID { get; private set; }

    public Ship Ship { get; private set; }
    public Planet Destination { get; private set; }

    public TripClock TripClock { get; private set; }
    private TravelClockFactory travelFactory;

    private float dockBuildTime;

    public Dock(DockData data)
    {
        dockName = data.dockName;
        dockState = data.dockState;
        DockID = data.dockID;
        Ship = data.ship;
        Destination = data.destination;

        if (data.resourcesInShip != null)
            Ship.resourcesModule = new ResourcesModule(data.resourcesInShip);

        var watchFactory = new WatchFactory();
        travelFactory = watchFactory.CreateTravelFactory();
        
        if (dockState == DockState.Building)
            TripClock = travelFactory.CreateTripClock(data.buildTimeLeft);
    }

    public void Unlock()
    {
        if (DockState != DockState.Locked)
            return;

        UpdateState(DockState.Unlocked);
    }

    public void StartBuilding()
    {
        dockBuildTime = Settlement.Instance.CosmicPort.DockBuildTime;

        TripClock = travelFactory.CreateTripClock(dockBuildTime);

        UpdateState(DockState.Building);
    }

    public void UpdateState(DockState newState)
    {
        if (DockState == newState)
            return;

        dockState = newState;

        onStateChange?.Invoke(DockState);
    }

    public void SetDestination(Planet newDestination)
    {
        if (Destination != newDestination)
            Destination = newDestination;
    }

    public void ReceiveShip(Ship ship)
    {
        Ship = ship;
        UpdateState(DockState.Occupied);
    }

    public void AssignShip(Ship ship)
    {
        Ship = ship;
        UpdateState(DockState.Occupied);
    }

    public void RemoveShip()
    {
        Ship = null;
        Destination = default;
        UpdateState(DockState.Empty);
    }
}

[Serializable]
public class DockData
{
    public string dockName;
    public DockState dockState;
    public DockID dockID;
    public Ship ship;
    public Planet destination;
    public List<Resource> resourcesInShip;
    public float buildTimeLeft;

    public DockData(string dockName, DockID dockID)
    {
        this.dockName = dockName;
        dockState = DockState.Locked;
        this.dockID = dockID;
        ship = null;
    }

    public DockData(Dock dock)
    {
        dockName = dock.dockName;
        dockState = dock.DockState;
        dockID = dock.DockID;
        ship = dock.Ship;
        destination = dock.Destination;
        
        if (dock.Ship != null && dock.Ship.resourcesModule != null)
            resourcesInShip = dock.Ship.resourcesModule.resources;
        
        if (dock.TripClock != null)
            buildTimeLeft = dock.TripClock.TimeLeft();
    }
}

public enum DockID
{
    A, B, C, D
}
