using System.Collections.Generic;
using UnityEngine;

public class Province : MonoBehaviour
{
    private List<Settlement> settlements;

    public void SetUpNewSettlement()
    {
        if (settlements == null)
            settlements = new List<Settlement>();

        var resources = new List<Resource>
        {
            new Resource(ResourceType.Type1, 0),
            new Resource(ResourceType.Type2, 0),
            new Resource(ResourceType.Type3, 0)
        };

        var newSettlement = new Settlement(resources, new List<Institution>(5), "firstSettlement");

        settlements.Add(newSettlement);
    }
}
