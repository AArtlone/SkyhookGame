using System.Collections.Generic;
using UnityEngine;

public class ManufactoryTasksViewController : ViewController
{
    [SerializeField] private ManufactoryTasksSelectableController selectableController = default;

    public override void WillAppear()
    {
        base.WillAppear();

        SetManufactoryTasksDataSet();
    }

    public void ChangeData()
    {
        if (IsShowing)
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
