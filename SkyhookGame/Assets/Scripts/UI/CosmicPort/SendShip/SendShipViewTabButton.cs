using MyUtilities.GUI;
using UnityEngine;
using UnityEngine.UI;

public class SendShipViewTabButton : TabButton
{
    [SerializeField] private Image icon = default;

    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }
}
