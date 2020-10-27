using System;
using TMPro;
using UnityEngine;

public class ManufactoryGridCell : SelectableCell<ManufactoryGridCellData>
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

        var callback = new Action(() =>
        {
            myButton.SetInteractable(true);

            var ship = new Ship(data.shipRecipe.shipName);

            Settlement.Instance.Manufactory.AddShipToStorage(ship);

            print("DONE BUILDING");
        });

        buildProgressBar.StartProgressBar(0, 2, callback);
    }
}

public class ManufactoryGridCellData : SelectableCellData
{
    public ShipRecipe shipRecipe;

    public ManufactoryGridCellData(ShipRecipe shipRecipe)
    {
        this.shipRecipe = shipRecipe;
    }
}
