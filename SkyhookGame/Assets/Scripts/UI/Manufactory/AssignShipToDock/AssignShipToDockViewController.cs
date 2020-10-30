using System.Collections.Generic;
using UnityEngine;

public class AssignShipToDockViewController : MonoBehaviour
{
    [SerializeField] private AssignShipToDockSelectableController selectableController = default;

    private bool isShowing;

    private void OnEnable()
    {
        isShowing = true;

        SetDocksDataSet();
    }

    private void OnDisable()
    {
        isShowing = false;
    }

    public void ShowView(Ship shipToAssign)
    {
        selectableController.Smth(shipToAssign);

        gameObject.SetActive(true);

        if (Settlement.Instance.CosmicPort == null)
        {
            Debug.LogError("CosmicPort is null");
            return;
        }

        SetDocksDataSet();
    }

    public void ChangeData()
    {
        if (isShowing)
            SetDocksDataSet();
    }

    private void SetDocksDataSet()
    {
        var emptyDocks = Settlement.Instance.CosmicPort.GetEmptyDocks();

        List<AssignShipToDockCellData> dataSet = new List<AssignShipToDockCellData>(emptyDocks.Count);

        emptyDocks.ForEach(e => dataSet.Add(new AssignShipToDockCellData(e)));

        selectableController.SetDataSet(dataSet);
    }
}
