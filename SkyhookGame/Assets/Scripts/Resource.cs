public class Resource
{
    public ResourcesDSID ResourceType { get; private set; }

    public int Amount { get; private set; }

    public Resource(ResourcesDSID resourceID)
    {
        ResourceType = resourceID;
    }

    public void IncreaseAmount(int valueToAdd)
    {
        Amount += valueToAdd;
    }
}
