using TMPro;
using UnityEngine;

public class BuildShipView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText = default;
    [SerializeField] private TextMeshProUGUI priceText = default;
    [SerializeField] private TextMeshProUGUI reqResourcesText = default;

    public void ShowView(ShipRecipe shipRecipe)
    {
        nameText.text = shipRecipe.shipName;
        priceText.text = shipRecipe.price.ToString();
        //reqResourcesText.text = shipRecipe.reqResources.ToString();

        gameObject.SetActive(true);
    }
}
