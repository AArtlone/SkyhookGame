using System.Collections.Generic;
using UnityEngine;

public class CosmicPortAssignShipViewController : StorageViewController
{
    [SerializeField] private DocksSelectableConroller docksSelectableConroller = default;
    public override void ViewWillAppear()
    {
        base.ViewWillAppear();

        CosmicPortAssignShipSelectableController cosmicPortAssignShipSelectableController = selectableController as CosmicPortAssignShipSelectableController;

        cosmicPortAssignShipSelectableController.SetDockToAssignTo(docksSelectableConroller.GetSelectedCell().data.dock);
    }

    protected override void SetStoragDataSet()
    {
        List<Ship> shipsInStorage = Manufactory.GetOnlyShips();

        List<StorageCellData> dataSet = new List<StorageCellData>(shipsInStorage.Count);

        shipsInStorage.ForEach(e => dataSet.Add(new StorageCellData(e)));

        selectableController.SetDataSet(dataSet);
    }
}
