using System.Collections.Generic;
using UnityEngine;

public class FactoryPageViewController : ViewController
{
    [SerializeField] private FactorySelectableController selectableController = default;

    public override void WillAppear()
    {
        SetManufactoryDataSet();
    }

    private void SetManufactoryDataSet()
    {
        List<ShipRecipe> shipRecipes = Settlement.Instance.Manufactory.ShipRecipes;

        List<FactoryCellData> dataSet = new List<FactoryCellData>(shipRecipes.Count);

        shipRecipes.ForEach(e => dataSet.Add(new FactoryCellData(e)));

        selectableController.SetDataSet(dataSet);
    }
}
