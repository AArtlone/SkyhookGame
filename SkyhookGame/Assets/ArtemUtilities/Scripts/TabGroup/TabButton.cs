using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Action onTabSelected;

    [SerializeField] private TabGroup tabGroup = default;

    public Image BackgroundImage { get; private set; }

    private void Awake()
    {
        BackgroundImage = GetComponent<Image>();

        tabGroup.Subscribe(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.EnterTab(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.SelectTab(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.ExitTab();
    }

    public void Select(Color color)
    {
        UpdateVisual(color);

        if (onTabSelected != null)
            onTabSelected.Invoke();
    }

    public void Select(Sprite sprite)
    {
        UpdateVisual(sprite);

        if (onTabSelected != null)
            onTabSelected.Invoke();
    }

    public void Deselect()
    {

    }

    public void UpdateVisual(Color color, float alphaValue = 1)
    {
        var newColor = new Color(color.r, color.g, color.b, alphaValue);

        BackgroundImage.color = newColor;
    }

    public void UpdateVisual(Sprite sprite)
    {
        BackgroundImage.sprite = sprite;
    }
}
