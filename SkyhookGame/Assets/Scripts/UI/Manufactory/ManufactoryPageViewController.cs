using System.Collections.Generic;
using UnityEngine;

public class ManufactoryPageViewController : ViewController
{
    [SerializeField] private ManufactorySelectableController selectableController = default;

    public override void WillAppear()
    {
        SetManufactoryDataSet();
    }

    private void SetManufactoryDataSet()
    {
        List<ShipRecipe> shipRecipes = Settlement.Instance.Manufactory.ShipRecipes;

        List<ManufactoryCellData> dataSet = new List<ManufactoryCellData>(shipRecipes.Count);

        shipRecipes.ForEach(e => dataSet.Add(new ManufactoryCellData(e)));

        selectableController.SetDataSet(dataSet);
    }
}
