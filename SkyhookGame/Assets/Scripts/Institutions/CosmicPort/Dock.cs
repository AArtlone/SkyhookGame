using System;
using UnityEngine;

[Serializable]
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

    public Dock(string name)
    {
        dockName = name;

        dockState = DockState.Locked;

        var watchFactory = new WatchFactory();
        
        travelFactory = watchFactory.CreateTravelFactory();
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
}

public class DockData
{
    public string dockName;
    public DockState dockState;

    public DockData(string dockName, DockState dockState)
    {
        this.dockName = dockName;
        this.dockState = dockState;
    }
}
