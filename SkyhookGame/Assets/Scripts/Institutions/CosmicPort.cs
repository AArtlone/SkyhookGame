using System.Collections.Generic;
using UnityEngine;

public class CosmicPort : Institution
{
    [SerializeField] private Vector2Int availableDocksRange = default;
    [SerializeField] private Vector2Int loadSpeedRange = default;
    [SerializeField] private Vector2Int unloadSpeedRange = default;

    [Space(10f)]
    [SerializeField] private List<Dock> allDocks = default;

    private int availableDocks;
    private int loadSpeed;
    private int unloadSpeed;

    private void Start()
    {
        print(availableDocks);

        if (availableDocks > allDocks.Count)
        {
            Debug.LogError("Number of available docks is more than all docks count. Either reduce the available docks range or add more docks to the All Docks list on " + transform.name);
            enabled = false;
            return;
        }

        UpdateDocksAvailability();
    }

    private void UpdateDocksAvailability()
    {
        for (int i = 0; i < availableDocks; i++)
            allDocks[i].SetAvailable();
    }

    public override void Upgrade()
    {
        base.Upgrade();

        UpdateVariables();

        DebugVariables();

        UpdateDocksAvailability();
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
}
