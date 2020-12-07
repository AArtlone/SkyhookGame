using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUIPrefab : MonoBehaviour
{
    [SerializeField] private Image icon = default;
    [SerializeField] private TextMeshProUGUI amountText = default;

    public void SetInfo(Sprite icon, int amount, Resource resource)
    {
        this.icon.sprite = icon;
        UpdateAmountText(amount);

        resource.onAmountChanged += Resource_OnAmountChange;
    }

    private void Resource_OnAmountChange(int amount)
    {
        UpdateAmountText(amount);
    }

    private void UpdateAmountText(int amount)
    {
        amountText.text = amount.ToString();
    }
}
