public class ShipRecipe
{
    public ShipsDSID shipID;
    public string shipName;
    public int price;
    public int shipMass;
    public int reqAluminium;
    public int reqPlatinum;

    public ShipRecipe(ShipsDSID shipID, string shipName)
    {
        this.shipID = shipID;
        this.shipName = shipName;
    }
}
