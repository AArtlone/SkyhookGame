using UnityEngine;

public class ManufactoryViewController : ViewController
{
    [SerializeField] private ManufactoryTabGroup tabGroup = default;

    private void TabGroup_OnTabSelection(ViewController selectedView)
    {
        ManufactoryGUIManager.ShowTabPage(selectedView);
    }

    public override void ViewWillAppear()
    {
        base.ViewWillAppear();

        tabGroup.onTabSelection += TabGroup_OnTabSelection;
    }

    public override void ViewWillDisappear()
    {
        base.ViewWillDisappear();

        tabGroup.onTabSelection -= TabGroup_OnTabSelection;
    }

    private ManufactoryGUIManager ManufactoryGUIManager { get { return ManufactoryGUIManager.Instance; } }
}
