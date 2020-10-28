using System;
using TMPro;
using UnityEngine;

public class ManufactoryCell : SelectableCell<ManufactoryGridCellData>
{
    [SerializeField] private ProgressBar buildProgressBar = default;

    [SerializeField] private TextMeshProUGUI nameText = default; 
    [SerializeField] private TextMeshProUGUI priceText = default; 
    [SerializeField] private TextMeshProUGUI reqResourcesText = default;

    public override void Refresh()
    {
        nameText.text = data.shipRecipe.name;
        priceText.text = data.shipRecipe.price.ToString();
        reqResourcesText.text = data.shipRecipe.reqResources.ToString();
    }

    public void StartBuilding()
    {
        myButton.SetInteractable(false);

        Manufactory.AddTask();

        var callback = new Action(() =>
        {
            myButton.SetInteractable(true);

            var ship = new Ship(data.shipRecipe.shipName);

            Manufactory.DoneBuildingShip(ship);

            print("DONE BUILDING");
        });

        buildProgressBar.StartProgressBar(0, Manufactory.BuildDuration, callback);
    }

    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}

public class ManufactoryGridCellData : SelectableCellData
{
    public ShipRecipe shipRecipe;

    public ManufactoryGridCellData(ShipRecipe shipRecipe)
    {
        this.shipRecipe = shipRecipe;
    }
}
