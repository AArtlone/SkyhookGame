using MyUtilities.GUI;
using System.Collections.Generic;
using UnityEngine;

public class ManufactoryTasksViewController : ViewController
{
    [SerializeField] private ManufactoryTasksSelectableController selectableController = default;

    public override void ViewWillBeFocused()
    {
        base.ViewWillBeFocused();

        SetManufactoryTasksDataSet();

        Manufactory.onManufactoryTasksChange += Manufactory_OnShipsManufactoryTasksChange;
    }

    public override void ViewWillBeUnfocused()
    {
        base.ViewWillBeUnfocused();

        Manufactory.onManufactoryTasksChange -= Manufactory_OnShipsManufactoryTasksChange;
    }

    private void Manufactory_OnShipsManufactoryTasksChange()
    {
        ChangeData();
    }

    private void ChangeData()
    {
        if (IsShowing)
            SetManufactoryTasksDataSet();
    }

    private void SetManufactoryTasksDataSet()
    {
        var manufactoryTasks = Manufactory.ManufactoryTasks;

        List<ManufactoryTasksCellData> dataSet = new List<ManufactoryTasksCellData>(manufactoryTasks.Count);

        manufactoryTasks.ForEach(e => dataSet.Add(new ManufactoryTasksCellData(e)));

        selectableController.SetDataSet(dataSet);
    }

    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}
