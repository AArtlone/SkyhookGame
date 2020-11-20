using TMPro;
using UnityEngine;

public class BuildShipView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText = default;
    [SerializeField] private TextMeshProUGUI priceText = default;
    [SerializeField] private TextMeshProUGUI reqAluminiumText = default;
    [SerializeField] private TextMeshProUGUI reqPlatinumText = default;

    public void ShowView(ShipRecipe shipRecipe)
    {
        nameText.text = shipRecipe.shipName;
        priceText.text = shipRecipe.price.ToString();
        reqAluminiumText.text = shipRecipe.reqAluminium.ToString();
        reqPlatinumText.text = shipRecipe.reqPlatinum.ToString();

        gameObject.SetActive(true);
    }
}
