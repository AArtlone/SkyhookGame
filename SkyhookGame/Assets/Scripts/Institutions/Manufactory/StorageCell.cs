using TMPro;
using UnityEngine;

public class StorageCell : SelectableCell<StorageCellData>
{
    [SerializeField] private TextMeshProUGUI nameText = default;

    public override void Refresh()
    {
        nameText.text = data.ship.shipName;
    }
}

public class StorageCellData : SelectableCellData
{
    public Ship ship;

    public StorageCellData(Ship ship)
    {
        this.ship = ship;
    }
}
