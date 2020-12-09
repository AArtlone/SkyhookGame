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

    public void SetUpAdjuster(ResourcesDSID type)
    {
        var icon = Resources.Load<Sprite>($"UI/Icons/Resources/{type}");
        this.icon.sprite = icon;

        ResourceType = type;

        Amount = 0;
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
        //Amount = Mathf.Clamp(Amount, 0, resourceAmount);
        amountText.text = Amount.ToString();

        Settlement.Instance.ResourcesModule.IncreaseResource(ResourceType, +increaser);

        onResourceChange?.Invoke();
    }

    private void OnMorePress()
    {
        int resourceAmount = Settlement.Instance.ResourcesModule.GetResourceAmount(ResourceType);

        if (resourceAmount < increaser)
            return;

        Amount += increaser;
        //Amount = Mathf.Clamp(Amount, 0, resourceAmount);
        amountText.text = Amount.ToString();

        Settlement.Instance.ResourcesModule.IncreaseResource(ResourceType, -increaser);

        onResourceChange?.Invoke();
    }
}
