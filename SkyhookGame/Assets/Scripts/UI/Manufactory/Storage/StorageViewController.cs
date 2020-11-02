using System.Collections.Generic;
using UnityEngine;

public class StorageViewController : ViewController
{
    [SerializeField] private StorageSelectableController selectableController = default;

    public override void WillAppear()
    {
        base.WillAppear();

        SetStoragDataSet();
    }

    public void ChangeData()
    {
        if (IsShowing)
            SetStoragDataSet();
    }

    private void SetStoragDataSet()
    {
        List<Ship> shipsInStorage = Settlement.Instance.Manufactory.ShipsInStorage;

        List<StorageCellData> dataSet = new List<StorageCellData>(shipsInStorage.Count);

        shipsInStorage.ForEach(e => dataSet.Add(new StorageCellData(e)));

        selectableController.SetDataSet(dataSet);
    }
}
