using MyUtilities.GUI;

public class DocksSelectableConroller : SelectableController<DocksCell, DocksCellData>
{
    protected override void Cell_OnCellPress(SelectableCell<DocksCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        switch (cell.data.dock.DockState)
        {
            case DockState.Unlocked:
                DocksViewController.ShowBuildDockView();
                break;
            case DockState.Empty:
                DocksViewController.ShowAssignShipView();
                break;
            case DockState.Occupied:
                CosmicPortGUIManager.Instance.ShowSendShipView(GetSelectedCell().data.dock);
                break;
        }
    }

    private DocksViewController DocksViewController { get { return CosmicPortGUIManager.Instance.DocksViewController; } }
}
