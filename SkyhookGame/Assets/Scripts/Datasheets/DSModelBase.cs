using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DSModelBase<T1, T2>: ScriptableObject 
    where T1: DSRecordBase<T2>
    where T2: struct, IComparable
{ 
    [SerializeField] protected List<T1> allRecords;

    public void Initialize(string csvFilePath)
    {
        allRecords = new List<T1>();

        List<string[]> lines = CSVReader.GetLines(csvFilePath);

        lines.ForEach(l => allRecords.Add(CreateRecord(l)));

        allRecords.ForEach(e => Debug.Log(e.recordID));
    }

    /// <summary>
    /// A method that is used to override the record's consutrctor
    /// </summary>
    /// <param name="csvFileLine"></param>
    /// <returns></returns>
    protected abstract T1 CreateRecord(string[] csvFileLine);

    public List<T1> GetAllRecords()
    {
        return allRecords;
    }

    public T1 GetRecordByID(T2 id)
    {
        foreach (var v in allRecords)
        {
            if (v.recordID.Equals(id))
                return v;
        }

        return null;
    }
}
