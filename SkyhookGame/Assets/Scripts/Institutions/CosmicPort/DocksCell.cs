using System;
using UnityEngine;

public class DocksCell : SelectableCell<DocksCellData>
{
    [SerializeField] private ProgressBar buildProgressBar = default;

    private float dockBuildTime = 0;

    public override void Initialize()
    {
        dockBuildTime = Settlement.Instance.CosmicPort.DockBuildTime;
    }

    public override void Refresh()
    {
        myButton.SetButtonText(data.dock.DockState.ToString());

        if (data.dock.DockState == DockState.Locked ||
            data.dock.DockState == DockState.Building)
            myButton.SetInteractable(false);
    }

    public void StartBuilding()
    {
        myButton.SetInteractable(false);

        var callback = new Action(() =>
        {
            FinishBuilding();
        });

        buildProgressBar.StartProgressBar(0, dockBuildTime, callback);

        data.dock.UpdateState(DockState.Building);
    }

    private void FinishBuilding()
    {
        myButton.SetInteractable(true);

        data.dock.UpdateState(DockState.Empty);

        Refresh();
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
