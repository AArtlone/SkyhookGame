using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dock : MonoBehaviour
{
    [SerializeField] private MyButton button = default;
    [SerializeField] private TextMeshProUGUI statusText = default;
    
    private Image backgroundImage = default;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();

        if (backgroundImage == null)
        {
            Debug.LogError("Image component does not exist on " + gameObject.name);
            enabled = false;
            return;
        }
    }

    public void SetAvailable()
    {
        UpdateVisualToAvailable();

        button.SetInteractable(true);

        statusText.text = "Available";
    }

    private void UpdateVisualToAvailable()
    {
        var color = backgroundImage.color;

        backgroundImage.color = new Color(color.r, color.g, color.b, 1);
    }
}
