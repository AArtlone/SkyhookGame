﻿using TMPro;
using UnityEngine;

public class ManufactoryCell : SelectableCell<ManufactoryGridCellData>
{
    [SerializeField] private TextMeshProUGUI nameText = default; 
    [SerializeField] private TextMeshProUGUI priceText = default; 
    [SerializeField] private TextMeshProUGUI reqResourcesText = default;

    public override void Refresh()
    {
        nameText.text = data.shipRecipe.name;
        priceText.text = data.shipRecipe.price.ToString();
        reqResourcesText.text = data.shipRecipe.reqResources.ToString();
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
