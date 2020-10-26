using System.Collections.Generic;
using UnityEngine;

public class CosmicPort : Institution
{
    [SerializeField] private Vector2Int availableDocksRange = default;
    [SerializeField] private Vector2Int loadSpeedRange = default;
    [SerializeField] private Vector2Int unloadSpeedRange = default;


    [Space(10f)]
    [SerializeField] private GameObject preview = default;
    [SerializeField] private DocksView docksView = default;

    private int availableDocks;
    private int loadSpeed;
    private int unloadSpeed;

    protected override void Awake()
    {
        base.Awake();

        preview.SetActive(false);
        docksView.gameObject.SetActive(false);
    }

    public override void Upgrade()
    {
        base.Upgrade();

        UpdateVariables();

        DebugVariables();

        docksView.UpdateDocksAvailability(availableDocks);
    }

    protected override void InitializeMethod()
    {
        UpdateVariables();

        DebugVariables();
    }

    protected override void UpdateVariables()
    {
        availableDocks = LevelModule.Evaluate(availableDocksRange);
        loadSpeed = LevelModule.Evaluate(loadSpeedRange);
        unloadSpeed = LevelModule.Evaluate(unloadSpeedRange);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New available docks number = " + availableDocks);
        Debug.Log("New load speed = " + loadSpeed);
        Debug.Log("New unload speed = " + unloadSpeed);
    }

    public void ShowPreview()
    {
        preview.SetActive(true);
    }

    public void ShowDocksView()
    {
        docksView.UpdateDocksAvailability(availableDocks);

        docksView.gameObject.SetActive(true);
    }
}
