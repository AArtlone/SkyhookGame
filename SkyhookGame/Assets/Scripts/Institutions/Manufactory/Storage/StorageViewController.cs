using System.Collections.Generic;
using UnityEngine;

public class StorageViewController : MonoBehaviour
{
    [SerializeField] private StorageSelectableController selectableController = default;

    private bool isShowing;

    private void OnEnable()
    {
        isShowing = true;

        SetStoragDataSet();
    }

    private void OnDisable()
    {
        isShowing = false;
    }

    public void ChangeData()
    {
        if (isShowing)
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
