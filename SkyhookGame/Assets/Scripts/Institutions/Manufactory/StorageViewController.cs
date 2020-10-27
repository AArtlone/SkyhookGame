using System.Collections.Generic;
using UnityEngine;

public class StorageViewController : SelectableController<StorageGridCell, StorageGridCellData>
{
    [SerializeField] private GameObject emptyStorageText = default;

    private List<StorageGridCell> shipsInStorage = new List<StorageGridCell>();

    private void Awake()
    {
        if (shipsInStorage.Count == 0)
        {
            emptyStorageText.SetActive(true);
            return;
        }

        List<StorageGridCellData> dataSet = new List<StorageGridCellData>(shipsInStorage.Count);

        shipsInStorage.ForEach(e => dataSet.Add(new StorageGridCellData(e.data.shipName)));

        SetDataSet(dataSet);

        Initialize();
    }
}
