using System.Collections.Generic;
using UnityEngine;

public class DocksViewController : SelectableController<DocksCell, DocksCellData>
{
    [SerializeField] private GameObject buildDockView = default;

    private DocksCell selectedDock;

    private void Awake()
    {
        CloseBuildDockView();

        CosmicPort.onUpgrade += CosmicPort_OnUpgrade;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        SetDocksDataSet();

        RefreshView();
    }

    private void OnDestroy()
    {
        if (CosmicPort == null)
            return;

        CosmicPort.onUpgrade -= CosmicPort_OnUpgrade;
    }

    private void CosmicPort_OnUpgrade()
    {
        SetDocksDataSet();
    }

    protected override void Cell_OnCellPress(SelectableCell<DocksCellData> cell)
    {
        base.Cell_OnCellPress(cell);

        SelectDock(cell as DocksCell);

        switch (cell.data.dock.DockState)
        {
            case DockState.Unlocked:
                ShowBuildDockView();
                break;
            case DockState.Empty:
                // Show assign view
                break;
        }
    }

    public void RefreshData()
    {
        SetDocksDataSet();

        if (isShowing)
            RefreshView();
    }

    private void SetDocksDataSet()
    {
        List<DocksCellData> dataSet = new List<DocksCellData>(CosmicPort.AllDocks.Count);

        CosmicPort.AllDocks.ForEach(e => dataSet.Add(new DocksCellData(e)));

        SetDataSet(dataSet);
    }

    public void SelectDock(DocksCell docksCell)
    {
        if (selectedDock == docksCell)
            return;

        selectedDock = docksCell;
    }

    private void CloseBuildDockView()
    {
        buildDockView.SetActive(false);
    }

    public void ShowBuildDockView()
    {
        buildDockView.SetActive(true);
    }

    #region EditorButtons
    public void BuildDock()
    {
        CloseBuildDockView();

        CosmicPort.StartBuildingDock(selectedDock.data.dock);
    }

    public void ShowDocksView()
    {
        gameObject.SetActive(true);
    }
#endregion

    private CosmicPort CosmicPort { get 
        {
            if (!Settlement.Exists)
                return null;

            return Settlement.Instance.CosmicPort; 
        } 
    }
}
