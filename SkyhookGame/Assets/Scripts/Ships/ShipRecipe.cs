using UnityEngine;

[CreateAssetMenu(fileName = "Ship Recipe", menuName = "Ship Recipe")]
public class ShipRecipe : ScriptableObject
{
    public string shipName;
    public int price;
    public int reqResources;
}
