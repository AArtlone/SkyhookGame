﻿using System.Collections.Generic;
using UnityEngine;

public class StorageSelectableController : SelectableController<StorageCell, StorageCellData>
{
    [SerializeField] private GameObject emptyStorageText = default;

    protected override void Cell_OnCellPress(SelectableCell<StorageCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        ShowAssignShipToDockView();
    }

    protected override void Refresh()
    {
        emptyStorageText.SetActive(CheckIfStorageIsEmpty());

        base.Refresh();
    }

    private void ShowAssignShipToDockView()
    {
        ManufactoryGUIManager.Instance.AssignShipToDock.ShowView(GetSelectedCell().data.ship);
    }

    private bool CheckIfStorageIsEmpty()
    {
        List<Ship> shipsInStorage = Manufactory.ShipsInStorage;

        return (shipsInStorage.Count == 0);
    }

    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}