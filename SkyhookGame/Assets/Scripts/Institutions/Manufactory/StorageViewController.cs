using System.Collections.Generic;
using UnityEngine;

public class StorageViewController : SelectableController<StorageCell, StorageCellData>
{
    [SerializeField] private GameObject emptyStorageText = default;

    [SerializeField] private AssignShipToDockViewController assingShipToDockView = default;

    protected override void OnEnable()
    {
        base.OnEnable();

        SetStoragDataSet();

        RefreshView();
    }

    protected override void Cell_OnCellPress(SelectableCell<StorageCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        ShowAssignShipToDockView();
    }

    protected override void RefreshView()
    {
        emptyStorageText.SetActive(CheckIfStorageIsEmpty());

        base.RefreshView();
    }

    private void ShowAssignShipToDockView()
    {
        assingShipToDockView.ShowView(GetSelectedCell().data.ship);
    }

    public void RefreshData()
    {
        SetStoragDataSet();

        if (isShowing)
            RefreshView();
    }

    private bool CheckIfStorageIsEmpty()
    {
        List<Ship> shipsInStorage = Manufactory.ShipsInStorage;

        return (shipsInStorage.Count == 0);
    }

    private void SetStoragDataSet()
    {
        List<Ship> shipsInStorage = Manufactory.ShipsInStorage;

        List<StorageCellData> dataSet = new List<StorageCellData>(shipsInStorage.Count);

        shipsInStorage.ForEach(e => dataSet.Add(new StorageCellData(e)));

        print(shipsInStorage.Count);

        SetDataSet(dataSet);
    }

    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}
