using System.Collections.Generic;
using UnityEngine;

public class StorageViewController : ViewController
{
    [SerializeField] protected StorageSelectableController selectableController = default;

    public override void ViewWillBeFocused()
    {
        base.ViewWillBeFocused();

        SetStoragDataSet();

        Manufactory.onShipsInStorageChange += Manufactory_OnShipsInStorageChange;
    }

    public override void ViewWillBeUnfocused()
    {
        base.ViewWillBeUnfocused();

        Manufactory.onShipsInStorageChange -= Manufactory_OnShipsInStorageChange;
    }

    private void Manufactory_OnShipsInStorageChange()
    {
        ChangeData();
    }

    private void ChangeData()
    {
        if (IsShowing)
            SetStoragDataSet();
    }

    private void SetStoragDataSet()
    {
        List<Ship> shipsInStorage = Manufactory.ShipsInStorage;

        List<StorageCellData> dataSet = new List<StorageCellData>(shipsInStorage.Count);

        shipsInStorage.ForEach(e => dataSet.Add(new StorageCellData(e)));

        selectableController.SetDataSet(dataSet);
    }

    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}
