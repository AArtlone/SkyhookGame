public class Resource
{
    public ResourceType ResourceType { get; private set; }

    public int Amount { get; private set; }

    public Resource(ResourceType resourceType, int amount)
    {
        ResourceType = resourceType;
        Amount = amount;
    }

    public void AddResource(int valueToAdd)
    {
        Amount += valueToAdd;
    }
}
