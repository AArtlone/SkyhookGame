using MyUtilities.GUI;
using TMPro;
using UnityEngine;

public class ResourceAdjuster : MonoBehaviour
{
    public System.Action onResourceChange;

    [SerializeField] private MyButton lessButton = default;
    [SerializeField] private MyButton moreButton = default;

    [SerializeField] private TextMeshProUGUI amountText = default;

    public int Amount { get; private set; }
    public ResourcesDSID ResourceType { get; private set; }

    private const int increaser = 10;

    public void SetUpAdjuster(ResourcesDSID type)
    {
        //TODO: Later must also update the ICON
        ResourceType = type;

        Amount = 0;
        amountText.text = Amount.ToString();

        lessButton.onClick += OnLessPress;
        moreButton.onClick += OnMorePress;
    }

    private void OnLessPress()
    {
        int resourceAmount = Settlement.Instance.ResourcesModule.GetResourceAmount(ResourceType);

        Amount -= increaser;
        Amount = Mathf.Clamp(Amount, 0, resourceAmount);
        amountText.text = Amount.ToString();

        onResourceChange?.Invoke();
    }

    private void OnMorePress()
    {
        int resourceAmount = Settlement.Instance.ResourcesModule.GetResourceAmount(ResourceType);

        Amount += increaser;
        Amount = Mathf.Clamp(Amount, 0, resourceAmount);
        amountText.text = Amount.ToString();

        onResourceChange?.Invoke();
    }
}
