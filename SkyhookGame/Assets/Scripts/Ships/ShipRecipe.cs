public class ShipRecipe
{
    public ShipsDSID shipID;
    public string shipName;
    public int price;
    public int reqAluminium;
    public int reqPlatinum;

    public ShipRecipe(ShipsDSID shipID, string shipName, int price, int reqAluminium, int reqPlatinum)
    {
        this.shipID = shipID;
        this.shipName = shipName;
        this.price = price;
        this.reqAluminium = reqAluminium;
        this.reqPlatinum = reqPlatinum;
    }
}
