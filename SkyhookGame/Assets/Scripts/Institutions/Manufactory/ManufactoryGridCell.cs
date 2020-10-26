using UnityEngine;

public class ManufactoryGridCell : CellData<ManufactoryGridCellData>
{

}

public class CellData<T> : MonoBehaviour where T : CellData
{

}

public class ManufactoryGridCellData : CellData
{

}

public class CellData
{
    public bool IsSelected;
}
