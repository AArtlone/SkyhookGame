using MyUtilities.GUI;
using System.Collections.Generic;
using UnityEngine;

public class StorageSelectableController : SelectableController<StorageCell, StorageCellData>
{
    [SerializeField] private GameObject emptyStorageText = default;

    protected override void Cell_OnCellPress(SelectableCell<StorageCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        OnCellPressed(cell.data.ship.shipType);
    }

    // For override purpose
    protected virtual void OnCellPressed(ShipsDSID shipType)
    {
        if (shipType == ShipsDSID.Skyhook)
            return;

        ShowAssignShipToDockView();
    }

    protected override void Refresh()
    {
        emptyStorageText.SetActive(CheckIfStorageIsEmpty());

        base.Refresh();
    }

    private void ShowAssignShipToDockView()
    {
        InstitutionsUIManager.Instance.ManufactoryUIManager.ShowAssignShipToDockView();
    }

    private bool CheckIfStorageIsEmpty()
    {
        List<Ship> shipsInStorage = Manufactory.ShipsInStorage;

        return (shipsInStorage.Count == 0);
    }

    protected Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}
