using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DSModelBase<T1, T2>: ScriptableObject 
    where T1: DSRecordBase<T2>
    where T2: struct, IComparable
{
    [SerializeField] protected int some;    
    [SerializeField] protected List<T1> allRecords;

    public void Initialize()
    {
        allRecords = new List<T1>();
    }

    public void AddRecord(T1 record)
    {
        allRecords.Add(record);
    }
}
