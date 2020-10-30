using System;
using TMPro;
using UnityEngine;

public class ManufactoryTasksCell : SelectableCell<ManufactoryTasksCellData>
{
    [SerializeField] private ProgressBar progressBar = default;
    
    [SerializeField] private TextMeshProUGUI shipName = default;

    public override void Refresh()
    {
        shipName.text = data.task.shipToProduce.shipName;

        var elapsedTime = data.task.TripClock.ElapsedTime();
        var duration = data.task.TripClock.Duration;

        var barValue = elapsedTime / duration;

        progressBar.StartProgressBar(data.task.TripClock, barValue);
    }
}

public class ManufactoryTasksCellData : SelectableCellData
{
    public ManufactoryTask task;

    public ManufactoryTasksCellData(ManufactoryTask task)
    {
        this.task = task;
    }
}
