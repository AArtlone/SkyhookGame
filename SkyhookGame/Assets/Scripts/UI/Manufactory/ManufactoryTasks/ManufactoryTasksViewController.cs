using System.Collections.Generic;
using UnityEngine;

public class ManufactoryTasksViewController : MonoBehaviour
{
    [SerializeField] private ManufactoryTasksSelectableController selectableController = default;

    private bool isShowing;

    private void OnEnable()
    {
        isShowing = true;

        SetManufactoryTasksDataSet();
    }

    private void OnDisable()
    {
        isShowing = false;
    }

    public void ChangeData()
    {
        if (isShowing)
            SetManufactoryTasksDataSet();
    }

    private void SetManufactoryTasksDataSet()
    {
        var manufactoryTasks = Settlement.Instance.Manufactory.ManufactoryTasks;

        List<ManufactoryTasksCellData> dataSet = new List<ManufactoryTasksCellData>(manufactoryTasks.Count);

        manufactoryTasks.ForEach(e => dataSet.Add(new ManufactoryTasksCellData(e)));

        selectableController.SetDataSet(dataSet);
    }
}
