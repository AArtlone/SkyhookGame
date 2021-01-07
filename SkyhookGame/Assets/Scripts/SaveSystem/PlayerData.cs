using System.Collections.Generic;

public class PlayerData
{
    public SettlementData earthData;
    public SettlementData moonData;
    public SettlementData marsData;

    public PlayerData()
    {

    }

    public PlayerData(SettlementData earthData, SettlementData moonData, SettlementData marsData)
    {
        this.earthData = earthData;
        this.moonData = moonData;
        this.marsData = marsData;
    }

    public SettlementData GetSettlementData(Planet planet)
    {
        switch (planet)
        {
            case Planet.Earth:
                return earthData;

            case Planet.Moon:
                return moonData;

            case Planet.Mars:
                return marsData;
        }

        return null;
    }

    public void SaveSettlementData(Planet planet, SettlementData settlementData)
    {
        switch (planet)
        {
            case Planet.Earth:
                earthData = settlementData;
                break;

            case Planet.Moon:
                moonData = settlementData;
                break;

            case Planet.Mars:
                marsData = settlementData;
                break;
        }
    }

    public void SaveCosmicPortData(Planet planet, CosmicPortData cosmicPortData)
    {
        switch (planet)
        {
            case Planet.Earth:
                earthData.cosmicPortData = cosmicPortData;
                break;

            case Planet.Moon:
                moonData.cosmicPortData = cosmicPortData;
                break;

            case Planet.Mars:
                marsData.cosmicPortData = cosmicPortData;
                break;
        }
    }

    public void SaveSettlementResources(Planet planet, List<Resource> resources)
    {
        SettlementData oldData = null;

        switch (planet)
        {
            case Planet.Earth:
                oldData = earthData;
                break;

            case Planet.Moon:
                oldData = moonData;
                break;

            case Planet.Mars:
                oldData = marsData;
                break;
        }

        List<Resource> newResources = new List<Resource>(oldData.resources);

        for (int i = 0; i < newResources.Count; i++)
            newResources[i].ChangeAmount(resources[i].Amount);

        var newSettlementData = new SettlementData(oldData, newResources);

        switch (planet)
        {
            case Planet.Earth:
                earthData = newSettlementData;
                break;

            case Planet.Moon:
                moonData = newSettlementData;
                break;

            case Planet.Mars:
                marsData = newSettlementData;
                break;
        }
    }
}
