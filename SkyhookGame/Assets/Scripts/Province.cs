using System.Collections.Generic;
using UnityEngine;

public class Province : MonoBehaviour
{
    public List<Settlement> Settlements { get; private set; }

    //public void SetUpNewSettlement()
    //{
    //    if (Settlements == null)
    //        Settlements = new List<Settlement>();

    //    var resources = new List<Resource>
    //    {
    //        new Resource(ResourceType.Type1, 0),
    //        new Resource(ResourceType.Type2, 0),
    //        new Resource(ResourceType.Type3, 0)
    //    };

    //    var newSettlement = new Settlement(resources, new List<Institution>(5), "firstSettlement");

    //    Settlements.Add(newSettlement);
    //}
}
