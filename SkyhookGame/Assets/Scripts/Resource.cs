using System;
using UnityEngine;

[Serializable]
public class Resource
{
    [SerializeField] private ResourcesDSID resourceType;
    public ResourcesDSID ResourceType { get { return resourceType; } }

    [SerializeField] private int amount;
    public int Amount { get { return amount; } }

    public Resource(ResourcesDSID resourceID)
    {
        resourceType = resourceID;
    }

    public void IncreaseAmount(int valueToAdd)
    {
        amount += valueToAdd;
    }
}
