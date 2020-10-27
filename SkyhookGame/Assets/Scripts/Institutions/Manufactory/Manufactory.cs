using System.Collections.Generic;
using UnityEngine;

public class Manufactory : Institution
{
    [SerializeField] private Vector2Int tasksCapacityRange = default;
    [SerializeField] private Vector2Int storageCapacityRange = default;

    [SerializeField] private List<ShipRecipe> shipRecipes = default;

    public List<ShipRecipe> ShipRecipes { get { return shipRecipes; } }

    public List<Ship> ShipsInStorage { get; private set; } = new List<Ship>();

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

    public void AddShipToStorage(Ship ship)
    {
        ShipsInStorage.Add(ship);

        ManufactoryUIController.Instance.StorageViewController.Refresh();
    }
}
