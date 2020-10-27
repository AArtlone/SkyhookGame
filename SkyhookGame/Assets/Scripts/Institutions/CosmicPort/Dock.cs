using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dock : MonoBehaviour
{
    [SerializeField] private MyButton button = default;
    [SerializeField] private TextMeshProUGUI statusText = default;

    [SerializeField] private ProgressBar buildProgressBar = default;

    private DockState dockState;

    private Image backgroundImage = default;

    private void Awake()
    {
        button.onClick += OnDockPressed;
    }

    private void OnDestroy()
    {
        button.onClick -= OnDockPressed;
    }

    public void Unlock()
    {
        if (dockState != DockState.Locked)
            return;

        UpdateVisualToUnlocked();

        button.SetInteractable(true);

        UpdateState(DockState.Unlocked);
    }

    public void StartBuilding()
    {
        button.SetInteractable(false);

        var callback = new Action(() =>
        {
            FinishBuilding();
        });

        buildProgressBar.StartProgressBar(0, 5, callback);

        UpdateState(DockState.Building);
    }

    private void FinishBuilding()
    {
        button.SetInteractable(true);

        UpdateState(DockState.Empty);
    }

    private void UpdateState(DockState newState)
    {
        if (dockState == newState)
            return;

        dockState = newState;

        statusText.text = dockState.ToString();
    }

    public void OnDockPressed()
    {
        DocksView.Instance.SelectDock(this);

        switch (dockState)
        {
            case DockState.Unlocked:
                DocksView.Instance.ShowBuildDockView();
                break;
            case DockState.Empty:
                // Show assign view
                break;
        }
    }

    private void UpdateVisualToUnlocked()
    {
        if (backgroundImage == null)
            CacheBackgroundImage();

        var color = backgroundImage.color;

        backgroundImage.color = new Color(color.r, color.g, color.b, 1);
    }

    private void CacheBackgroundImage()
    {
        backgroundImage = GetComponent<Image>();

        if (backgroundImage == null)
        {
            Debug.LogError("Image component does not exist on " + gameObject.name);
            enabled = false;
            return;
        }
    }
}
