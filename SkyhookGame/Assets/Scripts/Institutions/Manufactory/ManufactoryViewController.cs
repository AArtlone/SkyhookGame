using System.Collections.Generic;
using UnityEngine;

public class ManufactoryViewController : SelectableController<ManufactoryCell, ManufactoryGridCellData>
{
    [Space(5f)]
    [SerializeField] private BuildShipView buildShipView = default;
    
    protected override void OnEnable()
    {
        base.OnEnable();

        SetStoragDataSet();

        RefreshView();
    }

    private void SetStoragDataSet()
    {
        List<ShipRecipe> shipRecipes = Settlement.Instance.Manufactory.ShipRecipes;

        List<ManufactoryGridCellData> dataSet = new List<ManufactoryGridCellData>(shipRecipes.Count);

        shipRecipes.ForEach(e => dataSet.Add(new ManufactoryGridCellData(e)));

        SetDataSet(dataSet);
    }

    protected override void Cell_OnCellPress(SelectableCell<ManufactoryGridCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        ShowBuildShipView(cell.data.shipRecipe);
    }

    private void ShowBuildShipView(ShipRecipe shipRecipe)
    {
        buildShipView.ShowView(shipRecipe);
    }

    public void BuildShip()
    {
        bool canBuild = Settlement.Instance.Manufactory.CanBuild();
        bool canStore = Settlement.Instance.Manufactory.CanStore();

        if (!canStore)
        {
            PopUpManager.CreateSingleButtonTextPopUp("The ship cannot be built because there wont be enough space in the storage", "Ok", new System.Action(() => { }));
            return;
        }

        if (!canBuild)
        {
            PopUpManager.CreateSingleButtonTextPopUp("The ship cannot be built because you are at your maximum production capacity", "Ok", new System.Action(() => { }));
            return;
        }

        buildShipView.gameObject.SetActive(false);

        GetSelectedCell().StartBuilding();
    }
}

