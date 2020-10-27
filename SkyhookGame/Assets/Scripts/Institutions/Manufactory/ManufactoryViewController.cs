using System.Collections.Generic;
using UnityEngine;

public class ManufactoryViewController : SelectableController<ManufactoryGridCell, ManufactoryGridCellData>
{
    [SerializeField] private List<ShipRecipe> shipRecipes = default;

    private void Awake()
    {
        List<ManufactoryGridCellData> dataSet = new List<ManufactoryGridCellData>(shipRecipes.Count);

        shipRecipes.ForEach(e => dataSet.Add(new ManufactoryGridCellData(e)));

        SetDataSet(dataSet);

        Initialize();
    }
}

