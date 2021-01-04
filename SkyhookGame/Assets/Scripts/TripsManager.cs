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
        //onTripFinished?.Invoke(tripToFinish);
    }

    public void StartNewTrip(Planet planetOfOrigin, Planet destination, int timeToDestination, Ship ship)
    {
        if (AllTrips == null)
            AllTrips = new List<Trip>();

        AllTrips.Add(new Trip(planetOfOrigin, destination, timeToDestination, ship));
    }
}

public class Trip
{
    public System.Action onArrived;

    public Planet planetOfOrigin;
    public Planet destination;
    public int timeToDestination;
    public Ship ship;

    public TripClock TripClock { get; private set; }
    private TravelClockFactory travelFactory;

    public Trip (Planet planetOfOrigin, Planet destination, int timeToDestination, Ship ship)
    {
        this.planetOfOrigin = planetOfOrigin;
        this.destination = destination;
        this.timeToDestination = timeToDestination;
        this.ship = ship;


        var watchFactory = new WatchFactory();
        travelFactory = watchFactory.CreateTravelFactory();

        TripClock = travelFactory.CreateTripClock(timeToDestination);
    }

    public void Arrived()
    {
        onArrived?.Invoke();
    }
}
