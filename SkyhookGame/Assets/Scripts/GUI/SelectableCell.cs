using UnityEngine;

public abstract class SelectableCell<T> : MonoBehaviour where T : SelectableCellData
{
    public T data;

    public void SetData(T data)
    {
        this.data = data;
    }

    public abstract void Refresh();
}

public class SelectableCellData
{
    public bool IsSelected;
}
