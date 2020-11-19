using MyUtilities.GUI;
using System.Collections.Generic;
using UnityEngine;

public class DocksViewController : ViewController
{
    [Space(10f)]
    [SerializeField] private DocksSelectableConroller selectableController = default;

    [SerializeField] private GameObject buildDockView = default;

    private void CosmicPort_OnUpgrade()
    {
        SetDocksDataSet();
    }

    public override void ViewWillAppear()
    {
        base.ViewWillAppear();

        SetDocksDataSet();

        selectableController.SetButtonsInteractable(false);

        CosmicPort.onUpgrade += CosmicPort_OnUpgrade;
    }

    public override void ViewWillDisappear()
    {
        base.ViewWillDisappear();

        selectableController.SetButtonsInteractable(false);

        if (CosmicPort == null)
            return;

        CosmicPort.onUpgrade -= CosmicPort_OnUpgrade;
    }

    public override void ViewFocused()
    {
        base.ViewFocused();

        selectableController.SetButtonsInteractable(true);
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

    public void ShowBuildDockView()
    {
        buildDockView.SetActive(true);
    }

    public void ShowAssignShipView()
    {
        CosmicPortGUIManager.Instance.ShowCosmicPortAssignShipView();
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
