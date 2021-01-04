using MyUtilities.GUI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SendShipViewTabGroup : TabGroup
{
    public System.Action<Planet> onDestinationChanged;
    
    private List<Planet> displayedDestinations;

    public bool IsInitialized { get; private set; }
    
    public override void Initialize()
    {
        base.Initialize();

        Planet[] p = (Planet[])System.Enum.GetValues(typeof(Planet));

        displayedDestinations = p.ToList();

        displayedDestinations.Remove(Settlement.Instance.Planet);

        for (int i = 0; i < tabButtons.Count; i++)
        {
            SendShipViewTabButton _button = (SendShipViewTabButton)tabButtons[i];

            Sprite icon = Resources.Load<Sprite>($"Sprites/PlanetIcons/{displayedDestinations[i]}");
            
            _button.SetData(icon, displayedDestinations[i]);
        }

        IsInitialized = true;
    }

    public override void SelectTab(TabButton tabButton)
    {
        if (selectedTab != null)
            selectedTab.Deselect();

        selectedTab = tabButton;

        ResetTabButtonsVisuals();

        int index = tabButton.transform.GetSiblingIndex();

        if (displayedDestinations != null)
        {
            Planet newDestination = displayedDestinations[index];
            onDestinationChanged?.Invoke(newDestination);
        }

        if (type == TabGroupType.SpriteBased)
            selectedTab.Select(activeSprite);
        else
            selectedTab.Select(activeColor);
    }

    public void SelectDestination(Planet destination)
    {
        var correspondingTabButton = Smth(destination);

        if (correspondingTabButton == null)
        {
            Debug.LogWarning("Smth is very wrong");
            return;
        }

        SelectTab(correspondingTabButton);
    }

    public void SelectFirstDestination()
    {
        SelectTab(tabButtons[0]);
    }

    private TabButton Smth(Planet destination)
    {
        foreach (var v in tabButtons)
        {
            SendShipViewTabButton _button = (SendShipViewTabButton)v;

            if (_button.assignedPlanet == destination)
                return v;
        }

        return null;
    }

    public void HideTabs()
    {
        tabButtons.ForEach(b => b.gameObject.SetActive(false));
    }
}
