using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmicPort : Institution, ISavable<List<DockData>>
{
    public Action onUpgrade;

    [Header("Cosmic Port")]
    [SerializeField] private Vector2Int availableDocksRange = default;
    [SerializeField] private Vector2Int loadSpeedRange = default;
    [SerializeField] private Vector2Int unloadSpeedRange = default;

    [SerializeField] private float dockBuildTime = default;

    public float DockBuildTime { get { return dockBuildTime; } }

    public List<Dock> AllDocks { get; private set; }

    private int availableDocks;
    private int loadSpeed;
    private int unloadSpeed;

    private IEnumerator Start()
    {
        yield return SceneLoader.Instance.WaitForLoading();

        InitializeMethod();
    }

    private void Update()
    {
        if (AllDocks == null)
            return;

        foreach (var dock in AllDocks)
        {
            if (dock.DockState != DockState.Building)
                continue;

            if (dock.TripClock.TimeLeft() > 0)
                continue;

            dock.UpdateState(DockState.Empty);

            CosmicPortGUIManager.Instance.DocksViewController.ChangeData();
            ManufactoryGUIManager.Instance.AssignShipToDock.ChangeData();
        }
    }

    protected override void InitializeMethod()
    {
        var playerData = PlayerDataManager.Instance.PlayerData;

        if (playerData == null)
        {
            Debug.LogWarning("PlayerData is null");
            InitializeNewDocks();
        }
        else
            SetSavableData(playerData.docksData);

        UpdateVariables();

        DebugVariables();

        UpdateDocksAvailability();
    }

    private void InitializeNewDocks()
    {
        AllDocks = new List<Dock>()
        {
            new Dock(new DockData("Dock 1")),
            new Dock(new DockData("Dock 2")),
            new Dock(new DockData("Dock 3")),
            new Dock(new DockData("Dock 4")),
        };
    }

    public override void Upgrade()
    {
        base.Upgrade();

        UpdateVariables();

        //DebugVariables();

        UpdateDocksAvailability();
    }

    private void UpdateDocksAvailability()
    {
        if (AllDocks == null)
        {
            Debug.LogWarning("AllDocks is null");
            return;
        }

        if (availableDocks > AllDocks.Count)
        {
            Debug.LogError("Number of available docks is more than all docks count. Either reduce the available docks range or add more docks to the All Docks list on " + transform.name);
            enabled = false;
            return;
        }

        for (int i = 0; i < availableDocks; i++)
            AllDocks[i].Unlock();

        onUpgrade?.Invoke();
    }

    public void StartBuildingDock(Dock dock)
    {
        dock.StartBuilding();
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

    public List<Dock> GetEmptyDocks()
    {
        List<Dock> emptyDocks = new List<Dock>();

        foreach (var dock in AllDocks)
        {
            if (dock.DockState == DockState.Empty)
                emptyDocks.Add(dock);
        }

        return emptyDocks;
    }

    public List<DockData> GetSavableData()
    {
        if (AllDocks == null)
            return null;

        var saveData = new List<DockData>(AllDocks.Count);

        AllDocks.ForEach(d => saveData.Add(new DockData(d)));

        return saveData;
    }

    public void SetSavableData(List<DockData> data)
    {
        AllDocks = new List<Dock>(data.Count);

        data.ForEach(d => AllDocks.Add(new Dock(d)));
    }
}
