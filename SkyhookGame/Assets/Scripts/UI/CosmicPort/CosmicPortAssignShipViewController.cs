using UnityEngine;

public class CosmicPortAssignShipViewController : StorageViewController
{
    [SerializeField] private DocksSelectableConroller docksSelectableConroller = default;
    public override void WillAppear()
    {
        base.WillAppear();

        CosmicPortAssignShipSelectableController cosmicPortAssignShipSelectableController = selectableController as CosmicPortAssignShipSelectableController;

        cosmicPortAssignShipSelectableController.SetDockToAssignTo(docksSelectableConroller.GetSelectedCell().data.dock);
    }
}
