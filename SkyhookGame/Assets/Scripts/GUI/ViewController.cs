using UnityEngine;

public abstract class ViewController : MonoBehaviour
{
    protected bool IsShowing { get; private set; }

    public virtual void WillDisappear()
    {

    }

    public virtual void Disappeared()
    {
        IsShowing = false;
    }

    public virtual void WillAppear()
    {

    }

    public virtual void Appeared()
    {
        IsShowing = true;
    }
}
