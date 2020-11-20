public class ShipRecipe
{
    public ShipsDSID shipID;
    public string shipName;
    public int price;

    public ShipRecipe(ShipsDSID shipID, string shipName, int price)
    {
        this.shipID = shipID;
        this.shipName = shipName;
        this.price = price;
    }
}
