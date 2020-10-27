using System.Collections.Generic;
using UnityEngine;

public class SelectableController<T1, T2> : MonoBehaviour
    where T1 : SelectableCell<T2>
    where T2 : SelectableCellData
{
    [SerializeField] private T1 cellPrefab = default;
    [SerializeField] private RectTransform contentContainer = default;

    private List<T2> dataSet;

    protected virtual void SetDataSet(List<T2> dataSet)
    {
        this.dataSet = dataSet;
    }

    protected virtual void Initialize()
    {
        foreach (T2 data in dataSet)
        {
            var cell = Instantiate(cellPrefab, contentContainer);

            cell.SetData(data);

            cell.Refresh();
        }
    }
}
