﻿using System.Collections.Generic;
using UnityEngine;

public class AssignShipToDockViewController : SelectableController<AssignShipToDockCell, AssignShipToDockCellData>
{
    [SerializeField] private GameObject noDocksText = default;

    private Ship shipToAssign;

    private List<Dock> emptyDocks;

    public void ShowView(Ship shipToAssign)
    {
        this.shipToAssign = shipToAssign;

        gameObject.SetActive(true);

        if (CosmicPort == null)
        {
            Debug.LogError("CosmicPort is null");
            return;
        }

        emptyDocks = CosmicPort.GetEmptyDocks();

        SetDocksDataSet(emptyDocks);

        RefreshView();
    }

    protected override void RefreshView()
    {
        noDocksText.SetActive(emptyDocks.Count == 0);

        base.RefreshView();
    }

    private void SetDocksDataSet(List<Dock> emptyDocks)
    {
        List<AssignShipToDockCellData> dataSet = new List<AssignShipToDockCellData>(emptyDocks.Count);
        
        emptyDocks.ForEach(e => dataSet.Add(new AssignShipToDockCellData(e)));

        SetDataSet(dataSet);
    }

    protected override void Cell_OnCellPress(SelectableCell<AssignShipToDockCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        cell.data.dock.AssignShip(shipToAssign);

        gameObject.SetActive(false);

        Manufactory.RemoveShipFromStorage(shipToAssign);
    }

    private CosmicPort CosmicPort { get { return Settlement.Instance.CosmicPort; } }
    private Manufactory Manufactory { get { return Settlement.Instance.Manufactory; } }
}
