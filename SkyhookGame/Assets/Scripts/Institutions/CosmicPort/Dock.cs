using System;

public class Dock
{
    public Action<DockState> onStateChange;

    public string dockName;
    public DockState DockState { get; private set; }

    private Ship ship;

    public TripClock TripClock { get; private set; }
    private TravelClockFactory travelFactory;

    private float dockBuildTime;

    public Dock(string name)
    {
        dockName = name;

        DockState = DockState.Locked;

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

        DockState = newState;

        onStateChange?.Invoke(DockState);
    }

    public void AssignShip(Ship ship)
    {
        this.ship = ship;

        UpdateState(DockState.Occupied);
    }
}
