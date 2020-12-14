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
}
