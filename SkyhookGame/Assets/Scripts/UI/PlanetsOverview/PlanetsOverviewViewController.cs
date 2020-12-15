using MyUtilities.GUI;
using System;

public class PlanetsOverviewViewController : ViewController
{
    private Planet currentPlanet;

    private void Awake()
    {
        currentPlanet = Settlement.Instance.Planet;
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
