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
}
