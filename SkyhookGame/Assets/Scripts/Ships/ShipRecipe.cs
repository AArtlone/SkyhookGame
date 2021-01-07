public class ShipRecipe
{
    public ShipsDSID shipID;
    public string shipName;
    public int price;
    public int shipMass;
    public int reqAluminium;
    public int reqPlatinum;

    public ShipRecipe(ShipsDSID shipID, string shipName, int price, int shipMass, int reqAluminium, int reqPlatinum)
    {
        this.shipID = shipID;
        this.shipName = shipName;
        this.price = price;
        this.shipMass = shipMass;
        this.reqAluminium = reqAluminium;
        this.reqPlatinum = reqPlatinum;
    }
}
