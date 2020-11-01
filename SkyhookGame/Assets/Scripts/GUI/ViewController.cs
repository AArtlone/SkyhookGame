using UnityEngine;

public abstract class ViewController : MonoBehaviour
{
    protected bool isShowing;

    public virtual void WillDisappear()
    {

    }

    public virtual void Disappeared()
    {
        isShowing = false;
    }

    public virtual void WillAppear()
    {

    }

    public virtual void Appeared()
    {
        isShowing = true;
    }
}
