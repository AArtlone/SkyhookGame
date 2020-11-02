using UnityEngine;

public class FactorySelectableController : SelectableController<FactoryCell, FactoryCellData>
{
    [Space(5f)]
    [SerializeField] private BuildShipView buildShipView = default;

    protected override void Cell_OnCellPress(SelectableCell<FactoryCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        ManufactoryGUIManager.Instance.FactoryViewController.ShowBuildShipView(cell.data.shipRecipe);
    }
}

