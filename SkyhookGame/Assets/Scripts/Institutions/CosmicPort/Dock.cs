using System;
using System.Collections.Generic;
using UnityEngine;

public class Dock
{
    public Action<DockState> onStateChange;

    public string dockName;

    [SerializeField] private DockState dockState;
    public DockState DockState { get { return dockState; } }

    public Ship Ship { get; private set; }

    public TripClock TripClock { get; private set; }
    private TravelClockFactory travelFactory;

    private float dockBuildTime;

    public Dock(DockData data)
    {
        dockName = data.dockName;
        dockState = data.dockState;
        Ship = data.ship;

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

    public void AssignShip(Ship ship)
    {
        Ship = ship;
        UpdateState(DockState.Occupied);
    }

    public void RemoveShip()
    {
        Ship = null;
        UpdateState(DockState.Empty);
    }
}

[Serializable]
public class DockData
{
    public string dockName;
    public DockState dockState;
    public Ship ship;
    public List<Resource> resourcesInShip;
    public float buildTimeLeft;

    public DockData(string dockName)
    {
        this.dockName = dockName;
        dockState = DockState.Locked;
        ship = null;
    }

    public DockData(Dock dock)
    {
        dockName = dock.dockName;
        dockState = dock.DockState;
        ship = dock.Ship;
        
        if (dock.Ship != null && dock.Ship.resourcesModule != null)
            resourcesInShip = dock.Ship.resourcesModule.resources;
        
        if (dock.TripClock != null)
            buildTimeLeft = dock.TripClock.TimeLeft();
    }
}
