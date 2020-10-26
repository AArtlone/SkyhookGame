using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Action onClick;

    [SerializeField] private UpdateMethod updateMethod = default;
    [Space(5f)]
    [SerializeField] private Image backgroundImage = default;
    [Space(5f)]
    [SerializeField] private Sprite idleSprite = default;
    [SerializeField] private Sprite hoverSprite = default;
    [SerializeField] private Sprite activeSprite = default;
    [Space(5f)]
    [SerializeField] private Color idleColor = default;
    [SerializeField] private Color hoverColor = default;
    [SerializeField] private Color activeColor = default;

    [Space(5f)]
    [SerializeField] private UnityEvent onClickEvent = default;

    private void Awake()
    {
        if (backgroundImage == null)
        {
            Debug.LogWarning("BackgroundImage is not set in the editor.");
            enabled = false;
            return;
        }

        if (updateMethod == UpdateMethod.Color)
            UpdateVisual(idleColor);
        else
            UpdateVisual(idleSprite);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (updateMethod == UpdateMethod.Color)
            UpdateVisual(hoverColor);
        else
            UpdateVisual(hoverSprite);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
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
}

public enum UpdateMethod
{
    Sprite,
    Color
}
