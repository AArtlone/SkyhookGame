using TMPro;
using UnityEngine;

public class StorageGridCell : SelectableCell<StorageGridCellData>
{
    [SerializeField] private TextMeshProUGUI nameText = default;

    public override void Refresh()
    {
        nameText.text = data.shipName;
    }
}

public class StorageGridCellData : SelectableCellData
{
    public string shipName;

    public StorageGridCellData(string shipName)
    {
        this.shipName = shipName;
    }
}
