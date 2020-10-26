using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class ShowIfAttribute : PropertyAttribute
{
    public string ComparedPropertyName { get; private set; }
    public object ComparedValue { get; private set; }
    public ComparisonType ComparisonType { get; private set; }

    public ShowIfAttribute(string comparedPropertyName, object comparedValue, ComparisonType comparisonType)
    {
        ComparedPropertyName = comparedPropertyName;
        ComparedValue = comparedValue;
        ComparisonType = comparisonType;
    }
}
