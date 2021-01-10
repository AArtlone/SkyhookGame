using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manufactory : Institution<ManufactoryData>
{
    public Action onShipsInStorageChange;
    public Action onManufactoryTasksChange;

    [SerializeField] private Vector2Int tasksCapacityRange = default;
    [SerializeField] private Vector2Int storageCapacityRange = default;

    [SerializeField] private float buildDuration = default;

    public List<ManufactoryTask> ManufactoryTasks { get; private set; } = new List<ManufactoryTask>();
    public List<Ship> ShipsInStorage { get; private set; } = new List<Ship>();
    public float BuildDuration { get { return buildDuration; } }
    public int SkyhooksInStorage { get; private set; }

    private List<ManufactoryTask> tasksToRemove; // A list of task to remove at the end of the frame

    private int storageCapacity;
    private int tasksCapacity;

    private IEnumerator Start()
    {
        yield return SceneLoader.Instance.WaitForLoading();

        InitializeMethod();
    }

    private void Update()
    {
        tasksToRemove = new List<ManufactoryTask>();

        foreach (var task in ManufactoryTasks)
        {
            if (task.TripClock.TimeLeft() > 0)
                continue;

            tasksToRemove.Add(task);
        }

        tasksToRemove.ForEach(e => 
        {
            if (ManufactoryTasks.Contains(e))
            {
                ManufactoryTasks.Remove(e);
                DoneBuildingShip(e);
            }
        });
    }

    #region Institution Overrides
    protected override ManufactoryData GetInstitutionSaveData()
    {
        var playerData = PlayerDataManager.Instance.PlayerData;
        SettlementData settlementData = null;


        if (playerData != null)
            settlementData = playerData.GetSettlementData(Settlement.Instance.Planet);

        var data = settlementData == null ? null : settlementData.manufactoryData;

        return data;
    }

    public override ManufactoryData CreatSaveData()
    {
        if (ShipsInStorage == null)
            return null;

        // Create ShipsInStorageData
        var shipsData = new List<Ship>(ShipsInStorage);

        var tasksData = new List<ManufactoryTaskData>(ManufactoryTasks.Count);
        ManufactoryTasks.ForEach(t => tasksData.Add(new ManufactoryTaskData(t)));

        // Create ManufactoryData
        var saveData = new ManufactoryData(LevelModule.Level, shipsData, tasksData);

        return saveData;
    }

    public override void SetSavableData(ManufactoryData data)
    {
        // Set Levels
        LevelModule.SetLevel(data.institutionLevel);

        // Set ManufactorTasks
        ManufactoryTasks = new List<ManufactoryTask>(data.tasksData.Count);
        data.tasksData.ForEach(d => ManufactoryTasks.Add(new ManufactoryTask(d)));

        // Set ShipsInStorage
        ShipsInStorage = new List<Ship>(data.shipsInStorageData);

        foreach (var storageItem in ShipsInStorage)
        {
            if (storageItem.shipType == ShipsDSID.Skyhook)
                SkyhooksInStorage++;
        }
    }

    protected override void UpdateVariables()
    {
        base.UpdateVariables();

        storageCapacity = LevelModule.Evaluate(storageCapacityRange);
        tasksCapacity = LevelModule.Evaluate(tasksCapacityRange);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New Storage Capacity number = " + storageCapacity);
        Debug.Log("New Tasks Capacity number = " + tasksCapacity);
    }
    #endregion

    private void DoneBuildingShip(ManufactoryTask task)
    {
        var ship = task.shipToProduce;

        if (ship.shipType == ShipsDSID.Skyhook)
            SkyhooksInStorage++;

        ShipsInStorage.Add(ship);

        onManufactoryTasksChange?.Invoke();

        onShipsInStorageChange?.Invoke();
    }

    public void RemoveShipFromStorage(Ship ship)
    {
        if (!ShipsInStorage.Contains(ship))
            return;

        ShipsInStorage.Remove(ship);

        onShipsInStorageChange?.Invoke();
    }

    public void StartBuildingShip(ShipRecipe shipRecipe)
    {
        var ship = new Ship(shipRecipe.shipID, shipRecipe.shipName, shipRecipe.shipMass);

        var manufactoryTask = new ManufactoryTask(ship);

        ManufactoryTasks.Add(manufactoryTask);

        onManufactoryTasksChange?.Invoke();
    }

    public void RemoveSkyhook()
    {
        if (SkyhooksInStorage == 0)
            return;

        foreach (var storageItem in ShipsInStorage)
        {
            if (storageItem.shipType == ShipsDSID.Skyhook)
            {
                //TODO: make this a method
                ShipsInStorage.Remove(storageItem);
                SkyhooksInStorage--;
                onShipsInStorageChange?.Invoke();
                return;
            }
        }
    }
    /// <summary>
    /// A list of ships in the storage with Skyhooks
    /// </summary>
    /// <returns></returns>
    public List<Ship> GetOnlyShips()
    {
        List<Ship> result = new List<Ship>(ShipsInStorage.Count);

        foreach (var storageItem in ShipsInStorage)
            if (storageItem.shipType != ShipsDSID.Skyhook)
                result.Add(storageItem);

        return result;
    }

    public bool CanBuild()
    {
        return ManufactoryTasks.Count < tasksCapacity;
    }

    public bool CanStore()
    {
        // We need to add the ships that are already in storage to the ships that are buing built, sine they will take the space in storage when they are done building
        return ManufactoryTasks.Count + ShipsInStorage.Count < storageCapacity;
    }
}

[Serializable]
public class ManufactoryData : InstitutionData
{
    public List<Ship> shipsInStorageData;
    public List<ManufactoryTaskData> tasksData;

    public ManufactoryData(int institutionLevel, List<Ship> shipsInStorageData, List<ManufactoryTaskData> tasksData)
    {
        this.institutionLevel = institutionLevel;
        this.shipsInStorageData = shipsInStorageData;
        this.tasksData = tasksData;
    }
}
