using System.Collections.Generic;
using UnityEngine;

public class DocksViewController : ViewController
{
    [SerializeField] private DocksSelectableConroller selectableController = default;

    [SerializeField] private GameObject buildDockView = default;

    private void SelectableController_OnSelectedDockChange(DocksCell selectedDock)
    {
        switch (selectedDock.data.dock.DockState)
        {
            case DockState.Unlocked:
                ShowBuildDockView();
                break;
            case DockState.Empty:
                // Show assign view
                break;
        }
    }

    private void CosmicPort_OnUpgrade()
    {
        SetDocksDataSet();
    }

    public override void WillAppear()
    {
        SetDocksDataSet();

        selectableController.onSelectionChange += SelectableController_OnSelectedDockChange;

        CosmicPort.onUpgrade += CosmicPort_OnUpgrade;
    }

    public override void WillDisappear()
    {
        if (CosmicPort == null)
            return;

        CosmicPort.onUpgrade -= CosmicPort_OnUpgrade;
    }

    public void ChangeData()
    {
        if (IsShowing)
            SetDocksDataSet();
    }

    private void SetDocksDataSet()
    {
        List<DocksCellData> dataSet = new List<DocksCellData>(CosmicPort.AllDocks.Count);

        CosmicPort.AllDocks.ForEach(e => dataSet.Add(new DocksCellData(e)));

        selectableController.SetDataSet(dataSet);
    }

    private void ShowBuildDockView()
    {
        buildDockView.SetActive(true);
    }

    private void CloseBuildDockView()
    {
        buildDockView.SetActive(false);
    }

    #region EditorButtons
    public void BuildDock()
    {
        CloseBuildDockView();

        CosmicPort.StartBuildingDock(selectableController.GetSelectedCell().data.dock);
    }
    #endregion

    private CosmicPort CosmicPort
    {
        get
        {
            if (!Settlement.Exists)
                return null;

            return Settlement.Instance.CosmicPort;
        }
    }
}
