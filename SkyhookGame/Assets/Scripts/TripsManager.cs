using MyUtilities;
using System.Collections.Generic;

public class TripsManager : PersistentSingleton<TripsManager>
{
    public System.Action<Trip> onTripFinished;
    public List<Trip> AllTrips { get; private set; }

    private List<Trip> tripsToRemove; // A list of trips to remove at the end of the frame

    protected override void Awake()
    {
        SetInstance(this);
    }

    private void Update()
    {
        if (AllTrips == null)
            return;

        tripsToRemove = new List<Trip>(AllTrips.Count);

        foreach (var trip in AllTrips)
        {
            if (trip.TripClock.TimeLeft() > 0)
                continue;

            tripsToRemove.Add(trip);
        }

        tripsToRemove.ForEach(trip =>
        {
            if (AllTrips.Contains(trip))
                FinishTrip(trip);
        });
    }

    private void FinishTrip(Trip tripToFinish)
    {
        print("Trips has finished " + tripToFinish.destination);

        AllTrips.Remove(tripToFinish);

        tripToFinish.Arrived();
    }

    public void StartNewTrip(Planet planetOfOrigin, Planet destination, int timeToDestination, Ship ship, Dock destinationDock)
    {
        if (AllTrips == null)
            AllTrips = new List<Trip>();

        AllTrips.Add(new Trip(planetOfOrigin, destination, timeToDestination, ship, destinationDock));
    }
}

public class Trip
{
    public System.Action onArrived;

    public Planet planetOfOrigin;
    public Planet destination;
    public int timeToDestination;
    public Ship ship;
    public Dock destinationDock;

    public TripClock TripClock { get; private set; }
    private TravelClockFactory travelFactory;

    public Trip (Planet planetOfOrigin, Planet destination, int timeToDestination, Ship ship, Dock destinationDock)
    {
        this.planetOfOrigin = planetOfOrigin;
        this.destination = destination;
        this.timeToDestination = timeToDestination;
        this.ship = ship;
        this.destinationDock = destinationDock;

        var watchFactory = new WatchFactory();
        travelFactory = watchFactory.CreateTravelFactory();

        TripClock = travelFactory.CreateTripClock(timeToDestination);
    }

    public void Arrived()
    {
        onArrived?.Invoke();

        if (destination == Settlement.Instance.Planet)
        {
            Settlement.Instance.CosmicPort.TestLandShip(this);
        }
        else
        {
            ChangeOtherCosmicPortDocksData();
            ChangeOtherSettlementResources();
        }
    }

    private void ChangeOtherCosmicPortDocksData()
    {
        List<Dock> destinationDocks = PlayerDataManager.Instance.GetDocksByPlanet(destination);

        foreach (var dock in destinationDocks)
        {
            if (dock.DockID != destinationDock.DockID)
                continue;

            dock.ReceiveShip(ship);
        }

        int cosmicPortLevel = Settlement.Instance.CosmicPort.LevelModule.Level;
        var newDocksData = new List<DockData>(destinationDocks.Count);
        destinationDocks.ForEach(d => newDocksData.Add(new DockData(d)));

        var cosmicPortData = new CosmicPortData(cosmicPortLevel, newDocksData);

        PlayerDataManager.Instance.PlayerData.SaveCosmicPortData(destination, cosmicPortData);
    }

    private void ChangeOtherSettlementResources()
    {
        PlayerDataManager.Instance.PlayerData.SaveSettlementResources(destination, ship.resourcesModule);
    }
}
