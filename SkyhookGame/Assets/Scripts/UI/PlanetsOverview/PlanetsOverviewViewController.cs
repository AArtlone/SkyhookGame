using MyUtilities.GUI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsOverviewViewController : ViewController
{
    [SerializeField] private PlanetsOverviewTripsManager planetsOverviewTripsManager;

    private Planet currentPlanet;

    private void Awake()
    {
        currentPlanet = Settlement.Instance.Planet;
    }

    public override void ViewWillBeFocused()
    {
        base.ViewWillBeFocused();

        planetsOverviewTripsManager.SpawnShips();
    }

    public override void ViewWillBeUnfocused()
    {
        base.ViewWillBeUnfocused();

        planetsOverviewTripsManager.DestroyAllShips();
    }

    public void SelectPlanet(Planet planet)
    {
        if (planet == currentPlanet)
            return;

        ShowConfirmationPopUp(planet);
    }

    private void ShowConfirmationPopUp(Planet planet)
    {

        string text = $"Do you really want to go to {planet}?";
        string button1Text = "Yes";
        string button2Text = "No";
        Action button1Callback = new Action(() =>
        {
            print($"Changing to {planet} settlement");
            
            MySceneManager.Instance.LoadNewSettlement(planet);
        });

        PopUpManager.CreateDoubleButtonTextPopUp(text,
            button1Text,
            button2Text,
            button1Callback);
    }
}

[Serializable]
public class PlanetsOverviewTripsManager
{
    //public List<PlanetsOverviewShip> AllShips { get; private set; }
    
    [Space(5f)]
    [SerializeField] private List<ClickablePlanet> allPlanets = default;
    [Space(5f)]
    [SerializeField] private Transform shipsContainer = default;
    [SerializeField] private PlanetsOverviewShip planetOverviewShipPrefab = default;

    private Dictionary<Planet, Vector2> planetPositions;
    private Dictionary<Trip, PlanetsOverviewShip> test;

    private bool initialized;

    public void SpawnShips()
    {
        if (!initialized)
            Initialize();

        var allTrips = TripsManager.Instance.AllTrips;

        if (allTrips == null)
            return;

        test = new Dictionary<Trip, PlanetsOverviewShip>(allTrips.Count);

        foreach (var trip in TripsManager.Instance.AllTrips)
        {
            var shipPrefab = UnityEngine.Object.Instantiate(planetOverviewShipPrefab, GetShipPos(), Quaternion.identity, shipsContainer);

            shipPrefab.Launch(trip, planetPositions[trip.destination]);

            test.Add(trip, shipPrefab);
        }

        TripsManager.Instance.onTripFinished += Smth;
    }

    public void DestroyAllShips()
    {
        TripsManager.Instance.onTripFinished -= Smth;

        if (test == null)
            return;

        foreach (var v in test)
            UnityEngine.Object.Destroy(v.Value.gameObject);
    }

    private void Smth(Trip trip)
    {
        UnityEngine.Object.Destroy(test[trip].gameObject);

        test.Remove(trip);

        foreach(var v in test)
        {
            Debug.Log(string.Format("{0} | {1}", v.Key, v.Value));
        }
    }

    private void Initialize()
    {
        planetPositions = new Dictionary<Planet, Vector2>(allPlanets.Count);

        foreach (var v in allPlanets)
            planetPositions[v.Planet] = v.transform.position;

        initialized = true;
    }

    private Vector2 GetShipPos()
    {
        return planetPositions[Settlement.Instance.Planet];
    }
}
