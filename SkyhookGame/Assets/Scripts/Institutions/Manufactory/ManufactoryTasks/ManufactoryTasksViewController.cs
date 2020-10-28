using System.Collections.Generic;
using UnityEngine;

public class ManufactoryTasksViewController : 
    SelectableController<ManufactoryTasksCell, ManufactoryTasksCellData>
{
    [SerializeField] private GameObject noTasksText = default;

    protected override void OnEnable()
    {
        base.OnEnable();

        SetTasksData();

        RefreshView();
    }

    public void RefreshData()
    {
        SetTasksData();

        if (isShowing)
            RefreshView();
    }

    protected override void RefreshView()
    {
        noTasksText.SetActive(CheckIfTasksAreEmpty());

        base.RefreshView();
    }

    private void SetTasksData()
    {
        var manufactoryTasks = Manufactory.ManufactoryTasks;

        List<ManufactoryTasksCellData> dataSet = new List<ManufactoryTasksCellData>(manufactoryTasks.Count);

        manufactoryTasks.ForEach(e => dataSet.Add(new ManufactoryTasksCellData(e)));

        SetDataSet(dataSet);
    }

    private bool CheckIfTasksAreEmpty()
    {
        return Manufactory.ManufactoryTasks.Count == 0;
    }

    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}
