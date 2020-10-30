using System.Collections.Generic;
using UnityEngine;

public class ManufactoryViewController : MonoBehaviour
{
    [SerializeField] private ManufactorySelectableController selectableController = default;

    private bool isShowing;

    private void OnEnable()
    {
        isShowing = true;

        SetManufactoryDataSet();
    }

    private void OnDisable()
    {
        isShowing = false;
    }

    public void ChangeData()
    {
        if (isShowing)
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
