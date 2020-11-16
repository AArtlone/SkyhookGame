using System.Collections.Generic;
using UnityEngine;

public class AssignShipToDockViewController : ViewController
{
    [SerializeField] private StorageSelectableController storageSelectableController = default;
    [SerializeField] private AssignShipToDockSelectableController selectableController = default;

    public override void ViewWillAppear()
    {
        base.ViewWillAppear();

        ShowView(storageSelectableController.GetSelectedCell().data.ship);
    }

    private void ShowView(Ship shipToAssign)
    {
        selectableController.ShipToAssign(shipToAssign);

        if (Settlement.Instance.CosmicPort == null)
        {
            Debug.LogError("CosmicPort is null");
            return;
        }

        SetDocksDataSet();
    }

    public void ChangeData()
    {
        if (IsShowing)
            SetDocksDataSet();
    }

    private void SetDocksDataSet()
    {
        var emptyDocks = Settlement.Instance.CosmicPort.GetEmptyDocks();

        List<AssignShipToDockCellData> dataSet = new List<AssignShipToDockCellData>(emptyDocks.Count);

        emptyDocks.ForEach(e => dataSet.Add(new AssignShipToDockCellData(e)));

        selectableController.SetDataSet(dataSet);
    }
}
