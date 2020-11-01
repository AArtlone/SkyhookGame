using System.Collections.Generic;
using UnityEngine;

public class ManufactoryTabGroup : TabGroup
{
    private List<ViewController> viewControllers;

    public override void Initialize()
    {
        viewControllers = new List<ViewController>(pages.Count);

        foreach (GameObject page in pages)
        {
            var viewController = page.GetComponent<ViewController>();

            viewControllers.Add(viewController);
        }

        // Base.Initialize() call SelectTab
        // We need to initialize viewcontrollers list before SelectTab is called 
        base.Initialize();
    }

    public override void SelectTab(TabButton tabButton)
    {
        if (selectedTab != null)
            selectedTab.Deselect();

        selectedTab = tabButton;

        ResetTabButtonsVisuals();

        int index = tabButton.transform.GetSiblingIndex();

        ManufactoryGUIManager.Instance.DisplayTabPage(viewControllers[index]);

        if (type == TabGroupType.SpriteBased)
            selectedTab.Select(activeSprite);
        else
            selectedTab.Select(activeColor);
    }
}
