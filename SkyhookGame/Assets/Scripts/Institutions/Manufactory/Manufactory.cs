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

    public List<ManufactoryTask> ManufactoryTasks { get; private set; } = new List<ManufactoryTask>();
    public List<Ship> ShipsInStorage { get; private set; } = new List<Ship>();

    private int storageCapacity;
    private int tasksCapacity;

    private List<ManufactoryTask> tasksToRemove; // A list of task to remove at the end of the frame

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

    private void DoneBuildingShip(ManufactoryTask task)
    {
        var ship = new Ship(task.shipToProduce.shipName);

        ShipsInStorage.Add(ship);

        ManufactoryGUIManager.Instance.StorageViewController.ChangeData();
        ManufactoryGUIManager.Instance.TasksViewController.ChangeData();
    }

    public void RemoveShipFromStorage(Ship ship)
    {
        if (!ShipsInStorage.Contains(ship))
            return;

        ShipsInStorage.Remove(ship);

        ManufactoryUIController.StorageViewController.ChangeData();
    }

    public void StartBuildingShip(ShipRecipe shipRecipe)
    {
        var ship = new Ship(shipRecipe.shipName);

        var manufactoryTask = new ManufactoryTask(ship);

        ManufactoryTasks.Add(manufactoryTask);

        ManufactoryUIController.TasksViewController.ChangeData();
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

    private ManufactoryGUIManager ManufactoryUIController { get { return ManufactoryGUIManager.Instance; } }
}
