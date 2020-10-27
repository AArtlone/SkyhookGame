using System;
using System.Collections.Generic;
using UnityEngine;

public class ManufactoryViewController : SelectableController<ManufactoryGridCell, ManufactoryGridCellData>
{
    [Space(5f)]
    [SerializeField] private BuildShipView buildShipView = default;

    private void Awake()
    {
        List<ShipRecipe> shipRecipes = Settlement.Instance.Manufactory.ShipRecipes;

        List<ManufactoryGridCellData> dataSet = new List<ManufactoryGridCellData>(shipRecipes.Count);

        shipRecipes.ForEach(e => dataSet.Add(new ManufactoryGridCellData(e)));

        SetDataSet(dataSet);

        Initialize();
    }

    protected override void Cell_OnCellPress(SelectableCell<ManufactoryGridCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        ShowBuildShipView(cell.data.shipRecipe);
    }

    private void ShowBuildShipView(ShipRecipe shipRecipe)
    {
        buildShipView.ShowView(shipRecipe);
    }

    public void BuildShip()
    {
        buildShipView.gameObject.SetActive(false);

        GetSelectedCell().StartBuilding();
    }
}

