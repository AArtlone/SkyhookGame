using MyUtilities.GUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceAdjuster : MonoBehaviour
{
    public System.Action onResourceChange;

    [SerializeField] private MyButton lessButton = default;
    [SerializeField] private MyButton moreButton = default;

    [SerializeField] private Image icon = default;
    [SerializeField] private TextMeshProUGUI amountText = default;

    public int Amount { get; private set; }
    public ResourcesDSID ResourceType { get; private set; }

    private const int increaser = 10;

    private Resource resource;

    public void SetUpAdjuster(Resource resource)
    {
        this.resource = resource;

        var icon = Resources.Load<Sprite>($"UI/Icons/Resources/{resource.ResourceType}");
        this.icon.sprite = icon;

        ResourceType = resource.ResourceType;

        Amount = resource.Amount;
        amountText.text = Amount.ToString();

        lessButton.onClick += OnLessPress;
        moreButton.onClick += OnMorePress;
    }

    private void OnLessPress()
    {
        if (Amount == 0)
            return;

        int resourceAmount = Settlement.Instance.ResourcesModule.GetResourceAmount(ResourceType);

        Amount -= increaser;
        amountText.text = Amount.ToString();

        Settlement.Instance.ResourcesModule.IncreaseResource(ResourceType, +increaser);

        resource.SetAmount(Amount);
        onResourceChange?.Invoke();
    }

    private void OnMorePress()
    {
        int resourceAmount = Settlement.Instance.ResourcesModule.GetResourceAmount(ResourceType);

        if (resourceAmount < increaser)
            return;

        Amount += increaser;
        amountText.text = Amount.ToString();

        Settlement.Instance.ResourcesModule.IncreaseResource(ResourceType, -increaser);

        resource.SetAmount(Amount);
        onResourceChange?.Invoke();
    }
}
