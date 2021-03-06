﻿using MyUtilities.GUI;
using UnityEngine;

public class AssignShipToDockSelectableController : SelectableController<AssignShipToDockCell, AssignShipToDockCellData>
{
    [SerializeField] private GameObject noDocksText = default;

    private Ship shipToAssign;

    public void ShipToAssign(Ship shipToAssign)
    {
        this.shipToAssign = shipToAssign;
    }

    protected override void Cell_OnCellPress(SelectableCell<AssignShipToDockCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        cell.data.dock.AssignShip(shipToAssign);

        InstitutionsUIManager.Instance.ManufactoryUIManager.PopTopViewController();

        Manufactory.RemoveShipFromStorage(shipToAssign);
    }

    protected override void Refresh()
    {
        var emptyDocks = Settlement.Instance.CosmicPort.GetEmptyDocks();

        noDocksText.SetActive(emptyDocks.Count == 0);

        base.Refresh();
    }

    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}
