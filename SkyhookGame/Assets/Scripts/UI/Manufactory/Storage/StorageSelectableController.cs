using System.Collections.Generic;
using UnityEngine;

public class StorageSelectableController : SelectableController<StorageCell, StorageCellData>
{
    [SerializeField] private GameObject emptyStorageText = default;

    protected override void Cell_OnCellPress(SelectableCell<StorageCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        OnCellPressed();
    }

    protected virtual void OnCellPressed()
    {
        ShowAssignShipToDockView();
    }

    protected override void Refresh()
    {
        emptyStorageText.SetActive(CheckIfStorageIsEmpty());

        base.Refresh();
    }

    private void ShowAssignShipToDockView()
    {
        ManufactoryGUIManager.Instance.ShowAssignShipToDockView();
    }

    private bool CheckIfStorageIsEmpty()
    {
        List<Ship> shipsInStorage = Manufactory.ShipsInStorage;

        return (shipsInStorage.Count == 0);
    }

    protected Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}
