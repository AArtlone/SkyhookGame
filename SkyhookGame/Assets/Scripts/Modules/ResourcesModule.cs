using System.Collections.Generic;

public class ResourcesModule
{
    public List<Resource> resources { get; private set; }

    public ResourcesModule(List<ResourcesDSID> neededResources)
    {
        resources = new List<Resource>(neededResources.Count);

        foreach (var resourceID in neededResources)
        {
            var resource = new Resource(resourceID);

            resources.Add(resource);
        }
    }

    public ResourcesModule()
    {
        List<ResourcesDSID> allResourcesIDs = DSModelManager.Instance.ResourcesModel.GetAllResourcesIDs();
        
        resources = new List<Resource>();

        foreach (var resourceID in allResourcesIDs)
        {
            var resource = new Resource(resourceID);

            resources.Add(resource);
        }
    }

    public int GetResourceAmount(ResourcesDSID resourceType)
    {
        foreach (var resource in resources)
        {
            if (resource.ResourceType.Equals(resourceType))
                return resource.Amount;
        }

        return 0;
    }

    public void IncreaseResource(ResourcesDSID resourceType, int amountToAdd)
    {
        foreach (var resource in resources)
        {
            if (resource.ResourceType.Equals(resourceType))
                resource.IncreaseAmount(amountToAdd);
        }
    }
}
