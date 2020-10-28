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
}
