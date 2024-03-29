﻿using MyUtilities.GUI;
using System.Collections.Generic;
using UnityEngine;

public class FactoryViewController : ViewController
{
    [SerializeField] private FactorySelectableController selectableController = default;
    [SerializeField] private BuildShipView buildShipView = default;

    public override void ViewWillBeFocused()
    {
        base.ViewWillBeFocused();

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
            PopUpManager.CreateSingleButtonTextPopUp("The ship cannot be built because there wont be enough space in the storage", "Ok");
            return;
        }

        if (!canBuild)
        {
            var description = "The ship cannot be built because you are at your maximum production capacity";

            var buttonText = "Ok";

            PopUpManager.CreateSingleButtonTextPopUp(description, buttonText);
            return;
        }

        CloseBuildShipView();

        Manufactory.StartBuildingShip(selectableController.GetSelectedCell().data.shipRecipe);
    }

    private void SetManufactoryDataSet()
    {
        List<ShipRecipe> shipRecipes = DSModelManager.Instance.ShipsModel.GetShipRecipes();

        List<FactoryCellData> dataSet = new List<FactoryCellData>(shipRecipes.Count);

        shipRecipes.ForEach(e => dataSet.Add(new FactoryCellData(e)));

        selectableController.SetDataSet(dataSet);
    }

    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}
