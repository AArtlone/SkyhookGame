using UnityEngine;

public class CosmicPortAssignShipViewController : StorageViewController
{
    [SerializeField] private DocksSelectableConroller docksSelectableConroller = default;
    public override void ViewWillAppear()
    {
        base.ViewWillAppear();

        CosmicPortAssignShipSelectableController cosmicPortAssignShipSelectableController = selectableController as CosmicPortAssignShipSelectableController;

        cosmicPortAssignShipSelectableController.SetDockToAssignTo(docksSelectableConroller.GetSelectedCell().data.dock);
    }
}
