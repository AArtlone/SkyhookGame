using TMPro;
using UnityEngine;

public class ManufactoryUIController : Singleton<ManufactoryUIController>
{
    [Space(10f)]
    [SerializeField] private GameObject preview = default;
    [SerializeField] private GameObject upgradeView = default;
    [SerializeField] private GameObject manufactoryView = default;
    
    [Space(5f)]
    [SerializeField] private BuildShipView buildShipView = default;

    protected override void Awake()
    {
        SetInstance(this);

        preview.SetActive(false);
        upgradeView.SetActive(false);
        manufactoryView.gameObject.SetActive(false);
    }

    public void ShowBuildShipView(ShipRecipe shipRecipe)
    {
        buildShipView.ShowView(shipRecipe);
    }
}
