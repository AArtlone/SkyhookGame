using TMPro;
using UnityEngine;

public class StorageCell : SelectableCell<StorageCellData>
{
    [SerializeField] private TextMeshProUGUI nameText = default;

    public override void Refresh()
    {
        nameText.text = data.shipName;
    }
}

public class StorageCellData : SelectableCellData
{
    public string shipName;

    public StorageCellData(string shipName)
    {
        this.shipName = shipName;
    }
}
