using UnityEngine;

public class CosmicPortStorageSelectableController : StorageSelectableController
{
    [SerializeField] private DocksSelectableConroller docksSelectableConroller = default;

    protected override void OnCellPressed()
    {
        var ship = GetSelectedCell().data.ship;

        docksSelectableConroller.GetSelectedCell().data.dock.AssignShip(ship);

        Manufactory.RemoveShipFromStorage(ship);

        CosmicPortGUIManager.Instance.Back();
    }
}
