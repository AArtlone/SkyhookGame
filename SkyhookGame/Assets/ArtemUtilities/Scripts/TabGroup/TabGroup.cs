using System.Collections.Generic;
using UnityEngine;

public abstract class TabGroup : MonoBehaviour
{
    [SerializeField] private TabGroupType type = default;

    [ShowIf(nameof(type), nameof(TabGroupType.ColorBased), ComparisonType.Equals)]
    [SerializeField] private Color idleColor = default;
    [ShowIf(nameof(type), nameof(TabGroupType.ColorBased), ComparisonType.Equals)]
    [SerializeField] private Color hoverColor = default;
    [ShowIf(nameof(type), nameof(TabGroupType.ColorBased), ComparisonType.Equals)]
    [SerializeField] private Color activeColor = default;


    [ShowIf(nameof(type), nameof(TabGroupType.SpriteBased), ComparisonType.Equals)]
    [SerializeField] private Sprite idleSprite = default;
    [ShowIf(nameof(type), nameof(TabGroupType.SpriteBased), ComparisonType.Equals)]
    [SerializeField] private Sprite hoverSprite = default;
    [ShowIf(nameof(type), nameof(TabGroupType.SpriteBased), ComparisonType.Equals)]
    [SerializeField] private Sprite activeSprite = default;

    [SerializeField] private List<GameObject> pages = default;

    //[SerializeField] private EffectBase selectEffect = default;
    
    protected List<TabButton> tabButtons;

    protected TabButton selectedTab;
    
    private void Start()
    {
        if (tabButtons == null)
        {
            Debug.LogError("Tab buttons list is null. Make sure TabButtons call Subscribe method.");
            return;
        }

        ResetTabButtonsVisuals();

        ResetPages();

        int index = tabButtons[0].transform.GetSiblingIndex();

        SelectTab(tabButtons[index]);
    }

    public void Subscribe(TabButton tabButton)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();

        tabButtons.Add(tabButton);
    }

    public virtual void EnterTab(TabButton tabButton)
    {
        if (selectedTab == null || tabButton == selectedTab)
            return;

        if (type == TabGroupType.SpriteBased)
            tabButton.UpdateVisual(hoverSprite);
        else
            tabButton.UpdateVisual(hoverColor);
    }

    public virtual void SelectTab(TabButton tabButton)
    {
        if (selectedTab != null)
            selectedTab.Deselect();

        selectedTab = tabButton;

        ResetTabButtonsVisuals();

        int index = tabButton.transform.GetSiblingIndex();

        for (int i = 0; i < pages.Count; i++)
            pages[i].SetActive(index == i);

        if (type == TabGroupType.SpriteBased)
            selectedTab.Select(activeSprite);
        else
            selectedTab.Select(activeColor);
    }

    public void ExitTab()
    {
        ResetTabButtonsVisuals();
    }

    protected virtual void ResetTabButtonsVisuals()
    {
        foreach (TabButton button in tabButtons)
        {
            // We dont reset the selected tab button
            if (selectedTab != null && selectedTab == button)
                continue;

            if (type == TabGroupType.SpriteBased)
                button.UpdateVisual(idleSprite);
            else
                button.UpdateVisual(idleColor);
        }
    }

    private void ResetPages()
    {
        pages.ForEach(p => p.SetActive(false));
    }
}
