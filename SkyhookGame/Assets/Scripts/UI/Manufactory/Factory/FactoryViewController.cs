using System.Collections.Generic;
using UnityEngine;

public class FactoryViewController : ViewController
{
    [SerializeField] private FactorySelectableController selectableController = default;
    [SerializeField] private BuildShipView buildShipView = default;

    public override void ViewWillAppear()
    {
        base.ViewWillAppear();

        SetManufactoryDataSet();
    }

    public void ShowBuildShipView(ShipRecipe shipRecipe)
    {
        buildShipView.ShowView(shipRecipe);
    }

    private void CloseBuildShipView()
    {
        buildShipView.gameObject.SetActive(false);
    }

    public void BuildShip()
    {
        bool canBuild = Manufactory.CanBuild();
        bool canStore = Manufactory.CanStore();

        if (!canStore)
        {
            PopUpManager.CreateSingleButtonTextPopUp("The ship cannot be built because there wont be enough space in the storage", "Ok", new System.Action(() => { }));
            return;
        }

        if (!canBuild)
        {
            var description = "The ship cannot be built because you are at your maximum production capacity";

            var buttonText = "Ok";

            var callback = new System.Action(() =>
            {
                print("Hello World");
            });

            PopUpManager.CreateSingleButtonTextPopUp(description, buttonText, callback);
            return;
        }

        CloseBuildShipView();

        Manufactory.StartBuildingShip(selectableController.GetSelectedCell().data.shipRecipe);
    }

    private void SetManufactoryDataSet()
    {
        List<ShipRecipe> shipRecipes = Settlement.Instance.Manufactory.ShipRecipes;

        List<FactoryCellData> dataSet = new List<FactoryCellData>(shipRecipes.Count);

        shipRecipes.ForEach(e => dataSet.Add(new FactoryCellData(e)));

        selectableController.SetDataSet(dataSet);
    }

    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}
