public class Ship
{
    public ShipsDSID shipType;
    public string shipName;
    public int shipMass;
    public int totalWeight;

    public int fuel;

    public ResourcesModule resourcesModule;

    public Ship(ShipsDSID shipType, string shipName, int shipMass)
    {
        this.shipType = shipType;
        this.shipName = shipName;
        this.shipMass = shipMass;

        totalWeight = shipMass; // When ship is created the totalWeight = shipMass
    }
}
