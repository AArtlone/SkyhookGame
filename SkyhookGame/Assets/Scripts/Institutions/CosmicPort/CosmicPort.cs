﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmicPort : Institution<CosmicPortData>
{
    public Action onUpgrade;

    [Header("Cosmic Port")]
    [SerializeField] private Vector2Int availableDocksRange = default;
    [SerializeField] private Vector2Int loadSpeedRange = default;
    [SerializeField] private Vector2Int unloadSpeedRange = default;

    [SerializeField] private float dockBuildTime = default;

    [Space(5f)]
    [SerializeField] private ShipPrefab shipPrefab = default;
    [SerializeField] private ShipInSkyhoookPrefab shipInSkyhoookPrefab = default;
    [SerializeField] private Transform shipToLaunchContainer = default;
    [SerializeField] private Transform shipToLandContainer = default;
    
    [Space(5f)]
    [SerializeField] private GameObject smokeObject = default;

    public float DockBuildTime { get { return dockBuildTime; } }

    public List<Dock> AllDocks { get; private set; }

    private CosmicPortLandLaunchManager landLaunchManager;
    private CosmicPortSkyhookLandLaunchManager skyhookLandLaunchManager;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ManufactoryUIManager _man = ManufactoryUIManager.Instance as ManufactoryUIManager;
            //print(_man.AssignShipToDock);
            
            //var shipToLand = new Ship(ShipsDSID.CargoA, "SomeShip", 100);
            //LandShip(shipToLand);
        }

        if (AllDocks == null)
            return;

        foreach (var dock in AllDocks)
        {
            if (dock.DockState != DockState.Building)
                continue;

            if (dock.TripClock.TimeLeft() > 0)
                continue;

            dock.UpdateState(DockState.Empty);

            InstitutionsUIManager.CosmicPortUIManager.DocksViewController.ChangeData();
            InstitutionsUIManager.ManufactoryUIManager.AssignShipToDock.ChangeData();
        }
    }

    #region Institution Overrides
    public override void Upgrade()
    {
        base.Upgrade();

        UpdateDocksAvailability();
    }

    protected override void InitializeMethod()
    {
        base.InitializeMethod();

        landLaunchManager = new CosmicPortLandLaunchManager(this, shipPrefab, shipToLaunchContainer, shipToLandContainer);
        skyhookLandLaunchManager = new CosmicPortSkyhookLandLaunchManager(shipInSkyhoookPrefab);

        UpdateDocksAvailability();
    }

    protected override CosmicPortData GetInstitutionSaveData()
    {
        var playerData = PlayerDataManager.Instance.PlayerData;
        SettlementData settlementData = null;

        if (playerData != null)
            settlementData = playerData.GetSettlementData(Settlement.Instance.Planet);
        
        var data = settlementData == null ? null : settlementData.cosmicPortData;

        return data;
    }

    protected override void SaveDataIsNull()
    {
        InitializeNewDocks();
    }

    public override CosmicPortData CreateSaveData()
    {
        if (AllDocks == null)
            return null;

        // Create DocksData
        var docksData = new List<DockData>(AllDocks.Count);
        AllDocks.ForEach(d => docksData.Add(new DockData(d)));

        // Create CosmicPortData
        var saveData = new CosmicPortData(LevelModule.Level, docksData);

        return saveData;
    }

    public override void SetSavableData(CosmicPortData data)
    {
        // Set Levels
        print("Level is " + data.institutionLevel);
        int institutionLevel = data.institutionLevel;

        if (institutionLevel == 0)
            institutionLevel = 1;

        LevelModule.SetLevel(institutionLevel);

        // Set Docks
        AllDocks = new List<Dock>(data.docksData.Count);
        data.docksData.ForEach(d => AllDocks.Add(new Dock(d)));
    }

    protected override void UpdateVariables()
    {
        base.UpdateVariables();

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
    #endregion

    private void InitializeNewDocks()
    {
        AllDocks = new List<Dock>()
        {
            new Dock(new DockData("Dock 1", DockID.A)),
            new Dock(new DockData("Dock 2", DockID.B)),
            new Dock(new DockData("Dock 3", DockID.C)),
            new Dock(new DockData("Dock 4", DockID.D)),
        };
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

    public void LaunchShip(SendShipData sendShipData, LaunchMethod launchMethod)
    {
        if (launchMethod == LaunchMethod.Regular)
        {
            smokeObject.SetActive(true);
            landLaunchManager.LaunchShip(sendShipData, LevelModule.Level);
        }
        else
            skyhookLandLaunchManager.TryToLaunchShip(sendShipData);
    }

    public void LandShip(Trip trip)
    {
        landLaunchManager.LandShip(trip);
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

    private InstitutionsUIManager InstitutionsUIManager { get { return InstitutionsUIManager.Instance; } }
}

[Serializable]
public class CosmicPortData : InstitutionData
{
    public List<DockData> docksData;

    public CosmicPortData(int institutionLevel, List<DockData> docksData)
    {
        this.institutionLevel = institutionLevel;
        this.docksData = docksData;
    }
}

public struct SendShipData
{
    public Dock launchingDock;
    public Dock destinationDock;
    public Planet destination;

    public SendShipData(Dock launchingDock, Dock destinationDock, Planet destination)
    {
        this.launchingDock = launchingDock;
        this.destinationDock = destinationDock;
        this.destination = destination;
    }
}
