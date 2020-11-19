using MyUtilities.GUI;

public class FactorySelectableController : SelectableController<FactoryCell, FactoryCellData>
{
    protected override void Cell_OnCellPress(SelectableCell<FactoryCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        ManufactoryGUIManager.Instance.FactoryViewController.ShowBuildShipView(cell.data.shipRecipe);
    }
}

