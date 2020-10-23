using System.Collections.Generic;
using UnityEngine;

public class Settlement
{
    public List<Resource> resources;
    
    public List<Institution> institutions;
    
    public string Name { get; private set; }

    public int Level { get; private set; }
    public int Exp { get; private set; }

    private SettlementLevelUpSO levelUpSO;

    public Settlement(List<Resource> resources, List<Institution> institutions, string name)
    {
        this.resources = resources;
        this.institutions = institutions;

        Name = name;

        Level = 1;
        Exp = 0;

        levelUpSO = Resources.Load<SettlementLevelUpSO>("ScriptableObjects/SettlementLevelUpSO");
    }

    public void AddXp(int amountToAdd)
    {
        Exp += amountToAdd;

        // TODO: check for level increase

        int newLevel = levelUpSO.Evaluate(Exp);

        if (newLevel > Level)
            Level = newLevel;

        Debug.Log(Level);
    }
}
