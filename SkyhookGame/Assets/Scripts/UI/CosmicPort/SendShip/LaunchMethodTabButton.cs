using MyUtilities.GUI;
using UnityEngine;
using UnityEngine.UI;

public class LaunchMethodTabButton : TabButton
{
    [SerializeField] private Image image = default;

    [SerializeField] private Sprite selectectedSprite = default;
    [SerializeField] private Sprite unSelectectedSprite = default;

    public void SetCosmicPortSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public override void Select(Sprite sprite)
    {
        UpdateVisual(sprite);

        if (onTabSelected != null)
            onTabSelected.Invoke();
    }

    public override void Deselect()
    {
        UpdateVisual(unSelectectedSprite);
    }
}
