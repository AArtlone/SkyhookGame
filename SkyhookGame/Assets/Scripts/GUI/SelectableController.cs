using System.Collections.Generic;
using UnityEngine;

public class SelectableController<T1, T2> : MonoBehaviour
    where T1 : SelectableCell<T2>
    where T2 : SelectableCellData
{
    [SerializeField] private T1 cellPrefab = default;
    [SerializeField] private RectTransform contentContainer = default;

    private List<T1> cells;

    private List<T2> dataSet;

    private int selectedCell;

    protected virtual void SetDataSet(List<T2> dataSet)
    {
        this.dataSet = dataSet;
    }

    private void Initialize()
    {
        if (cells == null)
            cells = new List<T1>(dataSet.Count);

        foreach (T2 data in dataSet)
        {
            var cell = Instantiate(cellPrefab, contentContainer);

            cell.SetData(data);

            cell.Refresh();

            cell.onCellPress += Cell_OnCellPress;

            cells.Add(cell);
        }
    }

    /// <summary>
    /// Cleans up the cells list and destroys all cells, then Initializes cells again.
    /// Use this after updating dataSet
    /// </summary>
    protected void RefreshView()
    {
        if (cells != null)
            cells.ForEach(c => Destroy(c.gameObject));

        cells = null;

        Initialize();
    }

    protected virtual void Cell_OnCellPress(SelectableCell<T2> cell)
    {
        int selectedCellIndex = cells.IndexOf(cell as T1);

        selectedCell = selectedCellIndex;
    }

    public T1 GetSelectedCell()
    {
        return cells[selectedCell];
    }
}
