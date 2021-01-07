using MyUtilities.GUI;
using TMPro;
using UnityEngine;

public class FactoryCell : SelectableCell<FactoryCellData>
{
    [SerializeField] private TextMeshProUGUI nameText = default; 
    [SerializeField] private TextMeshProUGUI priceText = default; 
    [SerializeField] private TextMeshProUGUI reqAluminiumText = default;
    [SerializeField] private TextMeshProUGUI reqPlatinumText = default;

    private const string NamePrefix = "Name: ";
    private const string PricePrefix = "Price: ";
    private const string AluminiumPrefix = "Aluminium: ";
    private const string PlatinumPrefix = "Platinum: ";

    public override void Refresh()
    {
        nameText.text = NamePrefix + data.shipRecipe.shipName;

        priceText.text = PricePrefix + data.shipRecipe.price.ToString();

        reqAluminiumText.text = AluminiumPrefix + data.shipRecipe.reqAluminium.ToString();

        reqPlatinumText.text = PlatinumPrefix + data.shipRecipe.reqPlatinum.ToString();
    }
}

public class FactoryCellData : SelectableCellData
{
    public ShipRecipe shipRecipe;

    public FactoryCellData(ShipRecipe shipRecipe)
    {
        this.shipRecipe = shipRecipe;
    }
}
