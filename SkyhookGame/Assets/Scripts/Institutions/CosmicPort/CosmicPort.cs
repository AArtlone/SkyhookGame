using System;
using UnityEngine;

public class CosmicPort : Institution
{
    public Action<int> onAvailableDocksChanged;
    
    [SerializeField] private Vector2Int availableDocksRange = default;
    [SerializeField] private Vector2Int loadSpeedRange = default;
    [SerializeField] private Vector2Int unloadSpeedRange = default;

    public int AvailableDocks { get; private set; }
    private int loadSpeed;
    private int unloadSpeed;

    public override void Upgrade()
    {
        base.Upgrade();

        UpdateVariables();

        DebugVariables();

        onAvailableDocksChanged?.Invoke(AvailableDocks);
    }

    protected override void InitializeMethod()
    {
        UpdateVariables();

        DebugVariables();
    }

    protected override void UpdateVariables()
    {
        AvailableDocks = LevelModule.Evaluate(availableDocksRange);
        loadSpeed = LevelModule.Evaluate(loadSpeedRange);
        unloadSpeed = LevelModule.Evaluate(unloadSpeedRange);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New available docks number = " + AvailableDocks);
        Debug.Log("New load speed = " + loadSpeed);
        Debug.Log("New unload speed = " + unloadSpeed);
    }
}
