[System.Serializable]
public class ManufactoryTask
{
    public Ship shipToProduce;

    public TripClock TripClock { get; private set; }
    private TravelClockFactory travelFactory;

    public ManufactoryTask(Ship shipToProduce)
    {
        this.shipToProduce = shipToProduce;

        var watchFactory = new WatchFactory();

        travelFactory = watchFactory.CreateTravelFactory();

        var taskTime = Settlement.Instance.Manufactory.BuildDuration;

        TripClock = travelFactory.CreateTripClock(taskTime);
    }

    public ManufactoryTask(ManufactoryTaskData taskData)
    {
        shipToProduce = taskData.shipToProduce;

        var watchFactory = new WatchFactory();
        travelFactory = watchFactory.CreateTravelFactory();

        TripClock = travelFactory.CreateTripClock(taskData.buildTimeLeft);
    }
}

[System.Serializable]
public class ManufactoryTaskData
{
    public Ship shipToProduce;
    public float buildTimeLeft;

    public ManufactoryTaskData(ManufactoryTask manufactoryTask)
    {
        shipToProduce = manufactoryTask.shipToProduce;

        if (manufactoryTask.TripClock != null)
            buildTimeLeft = manufactoryTask.TripClock.TimeLeft();
    }
}
