using TMPro;
using UnityEngine;

public class StorageCell : SelectableCell<StorageCellData>
{
    [SerializeField] private TextMeshProUGUI nameText = default;

    public override void MyButton_OnClick()
    {
        throw new System.NotImplementedException();
    }

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
