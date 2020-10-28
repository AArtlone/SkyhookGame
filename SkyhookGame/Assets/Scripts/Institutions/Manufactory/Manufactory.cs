using System.Collections.Generic;
using UnityEngine;

public class Manufactory : Institution
{
    [SerializeField] private Vector2Int tasksCapacityRange = default;
    [SerializeField] private Vector2Int storageCapacityRange = default;

    [SerializeField] private List<ShipRecipe> shipRecipes = default;

    [SerializeField] private float buildDuration = default;

    public List<ShipRecipe> ShipRecipes { get { return shipRecipes; } }
    public float BuildDuration { get { return buildDuration; } }

    public List<Ship> ShipsInStorage { get; private set; } = new List<Ship>();

    private int tasks;
    private int storageCapacity;
    private int tasksCapacity;

    public override void Upgrade()
    {
        base.Upgrade();

        UpdateVariables();

        DebugVariables();
    }

    protected override void InitializeMethod()
    {
        UpdateVariables();

        DebugVariables();
    }

    protected override void UpdateVariables()
    {
        storageCapacity = LevelModule.Evaluate(storageCapacityRange);
        tasksCapacity = LevelModule.Evaluate(tasksCapacityRange);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New Storage Capacity number = " + storageCapacity);
        Debug.Log("New Tasks Capacity number = " + tasksCapacity);
    }

    public void DoneBuildingShip(Ship ship)
    {
        tasks--;
        
        ShipsInStorage.Add(ship);

        ManufactoryUIController.Instance.StorageViewController.RefreshData();
    }

    public void RemoveShipFromStorage(Ship ship)
    {
        if (!ShipsInStorage.Contains(ship))
            return;

        ShipsInStorage.Remove(ship);

        ManufactoryUIController.Instance.StorageViewController.RefreshData();
    }

    public void AddTask()
    {
        tasks++;
    }

    public bool CanBuild()
    {
        return tasks < tasksCapacity;
    }

    public bool CanStore()
    {
        // We need to add the ships that are already in storage to the ships that are buing built, sine they will take the space in storage when they are done building
        return tasks + ShipsInStorage.Count < storageCapacity;
    }
}
