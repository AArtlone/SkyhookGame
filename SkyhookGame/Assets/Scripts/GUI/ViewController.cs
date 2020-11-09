using UnityEngine;

public abstract class ViewController : MonoBehaviour
{
    [SerializeField] private bool hasChildNavController = default;
    [ShowIf(nameof(hasChildNavController), true, ComparisonType.Equals)]
    [SerializeField] private NavigationController childNavController = default;

    protected bool IsShowing { get; private set; }

    public virtual void ViewWillDisappear()
    {

    }

    public virtual void Disappeared()
    {

    }

    public virtual void ViewWillAppear()
    {

    }

    public virtual void ViewAppeared()
    {

    }

    public virtual void ViewWillBeFocused()
    {
    }

    public virtual void ViewFocused()
    {
        IsShowing = true;

        // This must be done in ViewFocused because it is called after the gameobject becomes active,
        // which is needed to start the coroutine inside FocusTopController()
        if (hasChildNavController)
            childNavController.FocusTopController();
    }

    public virtual void ViewWillBeUnfocused()
    {
        // This must be done in ViewWillBeUnfocused because it is called before the gameobject becomes inactive,
        // which is needed to start the coroutine inside UnfocusTopController()
        if (hasChildNavController)
            childNavController.UnfocusTopController();
    }

    public virtual void ViewUnfocused()
    {
        IsShowing = false;
    }
}
