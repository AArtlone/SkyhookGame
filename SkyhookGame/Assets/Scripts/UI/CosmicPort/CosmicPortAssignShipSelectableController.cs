﻿public class CosmicPortAssignShipSelectableController : StorageSelectableController
{
    private Dock dockToAssignTo;

    public void SetDockToAssignTo(Dock dock)
    {
        dockToAssignTo = dock;
    }

    protected override void OnCellPressed(ShipsDSID shipType)
    {
        var ship = GetSelectedCell().data.ship;

        dockToAssignTo.AssignShip(ship);

        Manufactory.RemoveShipFromStorage(ship);

        InstitutionsUIManager.Instance.CosmicPortUIManager.Back();
    }
}
