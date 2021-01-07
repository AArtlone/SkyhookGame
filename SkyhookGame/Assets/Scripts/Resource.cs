using System;
using UnityEngine;

[Serializable]
public class Resource
{
    public Action<int> onAmountChanged;

    [SerializeField] private ResourcesDSID resourceType;
    public ResourcesDSID ResourceType { get { return resourceType; } }

    [SerializeField] private int amount;
    public int Amount { get { return amount; } }

    public Resource(ResourcesDSID resourceID)
    {
        resourceType = resourceID;
    }

    public void ChangeAmount(int valueToAdd)
    {
        amount += valueToAdd;

        onAmountChanged?.Invoke(amount);
    }

    public void SetAmount(int value)
    {
        amount = value;

        onAmountChanged?.Invoke(amount);
    }
}
