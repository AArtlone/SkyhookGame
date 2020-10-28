using System;
using UnityEngine;

[RequireComponent(typeof(MyButton))]
public abstract class SelectableCell<T> : MonoBehaviour where T : SelectableCellData
{
    public Action<SelectableCell<T>> onCellPress;

    public T data;

    [HideInInspector] public MyButton myButton;

    public void SetData(T data)
    {
        this.data = data;

        myButton = GetComponent<MyButton>();

        myButton.onClick += MyButton_OnClick;
    }

    protected virtual void OnDestroy()
    {
        myButton.onClick -= MyButton_OnClick;
    }

    public abstract void Refresh();

    public virtual void Initialize() { }

    public virtual void MyButton_OnClick()
    {
        onCellPress?.Invoke(this);
    }
}

public class SelectableCellData
{

}
