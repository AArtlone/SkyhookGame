using System.Collections.Generic;
using UnityEngine;

public class StorageViewController : SelectableController<StorageCell, StorageCellData>
{
    [SerializeField] private GameObject emptyStorageText = default;

    private void OnEnable()
    {
        if (CheckIfStorageIsEmpty())
        {
            emptyStorageText.SetActive(true);
            return;
        }

        SetStoragDataSet();

        RefreshView();
    }

    public void RefreshData()
    {
        SetStoragDataSet();
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

        shipsInStorage.ForEach(e => dataSet.Add(new StorageCellData(e.shipName)));

        SetDataSet(dataSet);
    }

    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}
