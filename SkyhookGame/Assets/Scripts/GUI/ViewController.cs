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

    public virtual void ViewFocused()
    {
        IsShowing = true;

        if (hasChildNavController)
            childNavController.FocusTopController();
    }

    public virtual void ViewUnfocused()
    {
        IsShowing = false;
    }

    public virtual void ViewWillBeFocused()
    {

    }

    public virtual void ViewWillBeUnfocused()
    {
        if (hasChildNavController)
            childNavController.UnfocusTopController();
    }
}
