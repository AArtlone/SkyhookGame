using TMPro;
using UnityEngine;

public class AssignShipToDockCell : SelectableCell<AssignShipToDockCellData>
{
    [SerializeField] private TextMeshProUGUI dockName = default;

    public override void Refresh()
    {
        dockName.text = data.dock.dockName;

        myButton.SetButtonText(data.dock.DockState.ToString());
    }
}

public class AssignShipToDockCellData : SelectableCellData
{
    public Dock dock;

    public AssignShipToDockCellData(Dock dock)
    {
        this.dock = dock;
    }
}
