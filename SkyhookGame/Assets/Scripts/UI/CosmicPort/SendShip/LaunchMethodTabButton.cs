using MyUtilities.GUI;
using UnityEngine;
using UnityEngine.UI;

public class LaunchMethodTabButton : TabButton
{
    [SerializeField] private Image image = default;

    public void SetCosmicPortSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
