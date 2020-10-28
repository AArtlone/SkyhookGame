public class Dock
{
    public string dockName;
    public DockState DockState { get; private set; }

    private Ship ship;

    public Dock(string name)
    {
        dockName = name;

        DockState = DockState.Locked;
    }

    public void Unlock()
    {
        if (DockState != DockState.Locked)
            return;

        UpdateState(DockState.Unlocked);
    }

    public void UpdateState(DockState newState)
    {
        if (DockState == newState)
            return;

        DockState = newState;
    }

    public void AssignShip(Ship ship)
    {
        this.ship = ship;

        UpdateState(DockState.Occupied);
    }
}
