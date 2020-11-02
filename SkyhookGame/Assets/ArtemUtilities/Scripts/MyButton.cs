using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Action onClick;

    [SerializeField] private UpdateMethod updateMethod = default;

    [SerializeField] private Image backgroundImage = default;
    [SerializeField] private TextMeshProUGUI buttonText = default;
    [Space(5f)]
    [ShowIf(nameof(updateMethod), nameof(UpdateMethod.Sprite), ComparisonType.Equals)]
    [SerializeField] private Sprite idleSprite = default;
    [ShowIf(nameof(updateMethod), nameof(UpdateMethod.Sprite), ComparisonType.Equals)]
    [SerializeField] private Sprite hoverSprite = default;
    [ShowIf(nameof(updateMethod), nameof(UpdateMethod.Sprite), ComparisonType.Equals)]
    [SerializeField] private Sprite activeSprite = default;
    [Space(5f)]
    [ShowIf(nameof(updateMethod), nameof(UpdateMethod.Color), ComparisonType.Equals)]
    [SerializeField] private Color idleColor = default;
    [ShowIf(nameof(updateMethod), nameof(UpdateMethod.Color), ComparisonType.Equals)]
    [SerializeField] private Color hoverColor = default;
    [ShowIf(nameof(updateMethod), nameof(UpdateMethod.Color), ComparisonType.Equals)]
    [SerializeField] private Color activeColor = default;

    [Space(5f)]
    [SerializeField] private UnityEvent onClickEvent = default;

    public bool Interactable { get; private set; } = true;

    private void Awake()
    {
        if (backgroundImage == null)
        {
            Debug.LogWarning($"BackgroundImage is not set in the editor on { gameObject.name }");
            enabled = false;
            return;
        }

        if (!Interactable)
            return;

        if (updateMethod == UpdateMethod.Color)
            UpdateVisual(idleColor);
        else
            UpdateVisual(idleSprite);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!Interactable)
            return;

        if (updateMethod == UpdateMethod.Color)
            UpdateVisual(hoverColor);
        else
            UpdateVisual(hoverSprite);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        print("mouse clicked on " + gameObject.name);

        if (!Interactable)
            return;

        if (updateMethod == UpdateMethod.Color)
            UpdateVisual(activeColor);
        else
            UpdateVisual(activeSprite);

        if (onClickEvent != null)
            onClickEvent.Invoke();

        if (onClick != null)
            onClick.Invoke();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!Interactable)
            return;

        if (updateMethod == UpdateMethod.Color)
            UpdateVisual(idleColor);
        else
            UpdateVisual(idleSprite);
    }

    private void UpdateVisual(Sprite sprite)
    {
        backgroundImage.sprite = sprite;
    }

    private void UpdateVisual(Color colorToUpdate)
    {
        Color color = new Color(colorToUpdate.r, colorToUpdate.g, colorToUpdate.b, 1f);

        backgroundImage.color = color;
    }

    public void SetInteractable(bool value)
    {
        Interactable = value;

        if (Interactable)
        {
            if (updateMethod == UpdateMethod.Color)
                UpdateVisual(idleColor);
            else
                UpdateVisual(idleSprite);
        }
    }

    public void SetButtonText(string text)
    {
        if (buttonText == null)
        {
            Debug.LogWarning($"ButtonText object is not assigned in the editor or null on { gameObject.name }");
            return;
        }

        buttonText.text = text;
    }
}

public enum UpdateMethod
{
    Sprite,
    Color
}
