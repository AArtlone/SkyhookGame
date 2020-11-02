public class DocksSelectableConroller : SelectableController<DocksCell, DocksCellData>
{
    protected override void Cell_OnCellPress(SelectableCell<DocksCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        switch (cell.data.dock.DockState)
        {
            case DockState.Unlocked:
                CosmicPortGUIManager.Instance.DocksViewController.ShowBuildDockView();
                break;
            case DockState.Empty:
                CosmicPortGUIManager.Instance.DocksViewController.ShowAssignShipView();
                break;
        }
    }

    private DocksViewController DocksViewController { get { return CosmicPortGUIManager.Instance.DocksViewController; } }
}
