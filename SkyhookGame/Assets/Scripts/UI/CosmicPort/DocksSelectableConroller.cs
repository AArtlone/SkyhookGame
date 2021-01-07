using MyUtilities.GUI;

public class DocksSelectableConroller : SelectableController<DocksCell, DocksCellData>
{
    protected override void Cell_OnCellPress(SelectableCell<DocksCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        switch (cell.data.dock.DockState)
        {
            case DockState.Unlocked:
                CosmicPortUIManager.DocksViewController.ShowBuildDockView();
                break;
            case DockState.Empty:
                CosmicPortUIManager.DocksViewController.ShowAssignShipView();
                break;
            case DockState.Occupied:
                CosmicPortUIManager.ShowSendShipView(GetSelectedCell().data.dock);
                break;
        }
    }

    private CosmicPortUIManager CosmicPortUIManager { get { return InstitutionsUIManager.Instance.CosmicPortUIManager; } }
}
