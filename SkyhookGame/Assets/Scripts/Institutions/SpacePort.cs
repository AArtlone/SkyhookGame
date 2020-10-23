using UnityEngine;

public class SpacePort : Institution
{
    private int availableDocks;
    private int loadSpeed;
    private int unloadSpeed;

    private SpacePortLevelUpSO levelUpSO;

    public override void Upgrade()
    {
        base.Upgrade();

        UpdateVariables();

        DebugVariables();
    }

    protected override void InitializeMethod()
    {
        levelUpSO = Resources.Load<SpacePortLevelUpSO>("ScriptableObjects/SpacePortLevelUpSO");

        UpdateVariables();

        DebugVariables();
    }

    protected override void UpdateVariables()
    {
        availableDocks = levelUpSO.EvaluateDocks(Level);
        loadSpeed = levelUpSO.EvaluateLoadSpeed(Level);
        unloadSpeed = levelUpSO.EvaluateUnLoadSpeed(Level);
    }

    protected override void DebugVariables()
    {
        Debug.Log("New available docks number = " + availableDocks);
        Debug.Log("New load speed = " + loadSpeed);
        Debug.Log("New unload speed = " + unloadSpeed);
    }
}
