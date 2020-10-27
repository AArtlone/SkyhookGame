using TMPro;
using UnityEngine;

public class ManufactoryGridCell : SelectableCell<ManufactoryGridCellData>
{
    [SerializeField] private MyButton button = default;

    [SerializeField] private TextMeshProUGUI nameText = default; 
    [SerializeField] private TextMeshProUGUI priceText = default; 
    [SerializeField] private TextMeshProUGUI reqResourcesText = default;

    private void Awake()
    {
        if (button == null)
        {
            Debug.LogError("MyButton component was not assigned in the editor on " + gameObject.name);
            return;
        }

        button.SetInteractable(true);
        button.onClick += Button_OnClick;
    }

    public void Button_OnClick()
    {
        ManufactoryUIController.Instance.ShowBuildShipView(data.shipRecipe);
    }

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
