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

    private const int defaultIncreaserAmount = 10;
    private int increaser;

    private Resource resource;

    private bool hasIncreaseLimit;
    private int increaseLimitAmount;

    private SendShipViewController sendShipViewController;

    public void SetUpAdjuster(Resource resource, SendShipViewController sendShipViewController)
    {
        SetUp(resource, defaultIncreaserAmount, sendShipViewController);
    }

    public void SetUpAdjuster(Resource resource, int incrementAmount, SendShipViewController sendShipViewController)
    {
        SetUp(resource, incrementAmount, sendShipViewController);
    }

    public void SetUpAdjuster(Resource resource, bool increaseLimit, int increaseLimitAmount, SendShipViewController sendShipViewController)
    {
        hasIncreaseLimit = increaseLimit;
        this.increaseLimitAmount = increaseLimitAmount;

        SetUp(resource, defaultIncreaserAmount, sendShipViewController);
    }

    private void SetUp(Resource resource, int incrementAmount, SendShipViewController sendShipViewController)
    {
        this.resource = resource;
        this.increaser = incrementAmount;
        this.sendShipViewController = sendShipViewController;

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

        if (hasIncreaseLimit && (increaser + sendShipViewController.CurrentNoFuelAmount) > increaseLimitAmount)
            return;

        Amount += increaser;
        amountText.text = Amount.ToString();

        Settlement.Instance.ResourcesModule.IncreaseResource(ResourceType, -increaser);

        resource.SetAmount(Amount);
        onResourceChange?.Invoke();
    }
}
