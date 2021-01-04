using MyUtilities.GUI;
using System;
using UnityEngine;

public class PlanetsOverviewViewController : ViewController
{
    [SerializeField] private PlanetsOverviewTripsManager planetsOverviewTripsManager = default;

    private Planet currentPlanet;

    private void Awake()
    {
        currentPlanet = Settlement.Instance.Planet;
    }

    public override void ViewWillAppear()
    {
        base.ViewWillAppear();

        planetsOverviewTripsManager.SpawnShips();
    }

    public override void ViewWillDisappear()
    {
        base.ViewWillDisappear();

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