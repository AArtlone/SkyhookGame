public class Dock
{
    public DockState DockState { get; private set; }

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
}
