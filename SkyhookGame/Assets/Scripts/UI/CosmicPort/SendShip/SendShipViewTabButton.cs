using MyUtilities.GUI;
using UnityEngine;
using UnityEngine.UI;

public class SendShipViewTabButton : TabButton
{
    [SerializeField] private Image icon = default;

    public Planet assignedPlanet { get; private set; }

    public void SetData(Sprite sprite, Planet planet)
    {
        icon.sprite = sprite;
        assignedPlanet = planet;
    }
}
