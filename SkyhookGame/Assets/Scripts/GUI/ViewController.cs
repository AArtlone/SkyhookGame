using UnityEngine;

public abstract class ViewController : MonoBehaviour
{
    protected bool IsShowing { get; private set; }

    public virtual void ViewWillDisappear()
    {

    }

    public virtual void Disappeared()
    {
        IsShowing = false;
    }

    public virtual void ViewWillAppear()
    {

    }

    public virtual void ViewAppeared()
    {
        IsShowing = true;
    }
}
