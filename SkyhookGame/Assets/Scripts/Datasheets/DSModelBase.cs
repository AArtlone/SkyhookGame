using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DSModelBase<T1, T2>: ScriptableObject where T1: DSRecordBase where T2: struct, IComparable
{
    private List<T1> allRecords = new List<T1>();
}

public abstract class DSRecordBase
{

}
