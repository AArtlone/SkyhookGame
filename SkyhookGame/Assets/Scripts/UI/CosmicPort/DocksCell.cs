using System;
using UnityEngine;

public class DocksCell : SelectableCell<DocksCellData>
{
    [SerializeField] private ProgressBar buildProgressBar = default;

    protected override void OnDestroy()
    {
        base.OnDestroy();

        data.dock.onStateChange -= Dock_OnStateChange;
    }

    private void Dock_OnStateChange(DockState state)
    {
        Refresh();
    }

    public override void Initialize()
    {
        base.Initialize();

        data.dock.onStateChange += Dock_OnStateChange;
    }

    public override void Refresh()
    {
        myButton.SetButtonText(data.dock.DockState.ToString());

        if (data.dock.DockState == DockState.Locked ||
            data.dock.DockState == DockState.Building)
            myButton.SetInteractable(false);
        else
            myButton.SetInteractable(true);

        if (data.dock.DockState == DockState.Building)
        {
            var elapsedTime = data.dock.TripClock.ElapsedTime();
            var duration = data.dock.TripClock.Duration;

            var barValue = elapsedTime / duration;

            buildProgressBar.StartProgressBar(data.dock.TripClock, barValue);
        }
    }
}

public class DocksCellData : SelectableCellData
{
    public Dock dock;

    public DocksCellData(Dock dock) 
    {
        this.dock = dock;
    }
}
