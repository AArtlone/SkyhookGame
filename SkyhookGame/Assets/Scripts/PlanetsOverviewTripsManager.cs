using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlanetsOverviewTripsManager
{
    [Space(5f)]
    [SerializeField] private List<ClickablePlanet> allPlanets = default;
    [Space(5f)]
    [SerializeField] private Transform shipsContainer = default;
    [SerializeField] private PlanetsOverviewShip planetOverviewShipPrefab = default;

    private Dictionary<Planet, Vector2> planetPositions;

    private List<GameObject> currentShips;

    private bool initialized;

    public void SpawnShips()
    {
        if (!initialized)
            Initialize();

        var allTrips = TripsManager.Instance.AllTrips;

        if (allTrips == null)
            return;

        currentShips = new List<GameObject>(allTrips.Count);

        foreach (var trip in TripsManager.Instance.AllTrips)
        {
            var shipPrefab = UnityEngine.Object.Instantiate(planetOverviewShipPrefab, GetShipPos(trip.planetOfOrigin), Quaternion.identity, shipsContainer);

            shipPrefab.Launch(trip, planetPositions[trip.destination]);

            currentShips.Add(shipPrefab.gameObject);
        }
    }

    public void DestroyAllShips()
    {
        if (currentShips == null)
            return;

        currentShips.ForEach(s => UnityEngine.Object.Destroy(s));
    }

    private void Initialize()
    {
        planetPositions = new Dictionary<Planet, Vector2>(allPlanets.Count);

        foreach (var v in allPlanets)
            planetPositions[v.Planet] = v.transform.position;

        initialized = true;
    }

    private Vector2 GetShipPos(Planet planetOfOrigin)
    {
        return planetPositions[planetOfOrigin];
    }
}
