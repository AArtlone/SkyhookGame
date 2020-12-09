using MyUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripsManager : Singleton<TripsManager>
{
    private List<Trip> allTrips;
    protected override void Awake()
    {
        SetInstance(this);
    }

    public void StartNewTrip(string destination, int timeToDestination, Ship ship)
    {
        if (allTrips == null)
            allTrips = new List<Trip>();

        allTrips.Add(new Trip(destination, timeToDestination, ship));
    }
}

public class Trip
{
    public string destination;
    public int timeToDestination;
    public Ship ship;

    public Trip (string destination, int timeToDestination, Ship ship)
    {
        this.destination = destination;
        this.timeToDestination = timeToDestination;
        this.ship = ship;
    }
}
