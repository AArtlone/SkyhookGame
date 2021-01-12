using MyUtilities.GUI;
using System.Collections.Generic;
using UnityEngine;

public class LaunchMethodTabGroup : TabGroup
{
    public System.Action<LaunchMethod> onLaunchTypeChanged;

    [Space(5f)]
    [SerializeField] private List<Sprite> cosmicPortSprites = default;

    public bool IsInitialized { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        LaunchMethodTabButton launchMethodTabButton = (LaunchMethodTabButton)tabButtons[0];

        int cosmicPortLevel = Settlement.Instance.CosmicPort.LevelModule.Level;
        var spriteToAssign = cosmicPortSprites[cosmicPortLevel - 1];

        launchMethodTabButton.SetCosmicPortSprite(spriteToAssign);

        IsInitialized = true;
    }

    public void ToggleSkyhookButton()
    {
        tabButtons[1].gameObject.SetActive(SkyhookManager.Instance.SkyhookIsInstalled);
    }

    public override void SelectTab(TabButton tabButton)
    {
        if (selectedTab != null)
            selectedTab.Deselect();

        selectedTab = tabButton;

        ResetTabButtonsVisuals();

        int index = tabButton.transform.GetSiblingIndex();

        if (index == 0)
            onLaunchTypeChanged?.Invoke(LaunchMethod.Regular);
        else if (index == 1)
            onLaunchTypeChanged?.Invoke(LaunchMethod.Skyhook);

        if (type == TabGroupType.SpriteBased)
            selectedTab.Select(activeSprite);
        else
            selectedTab.Select(activeColor);
    }
}

public enum LaunchMethod
{
    Regular, Skyhook
}
