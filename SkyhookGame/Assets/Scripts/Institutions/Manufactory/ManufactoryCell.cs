using TMPro;
using UnityEngine;

public class ManufactoryCell : SelectableCell<ManufactoryCellData>
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

public class ManufactoryCellData : SelectableCellData
{
    public ShipRecipe shipRecipe;

    public ManufactoryCellData(ShipRecipe shipRecipe)
    {
        this.shipRecipe = shipRecipe;
    }
}
