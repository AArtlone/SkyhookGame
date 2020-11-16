using System;

public abstract class DSRecordBase<T> where T: struct, IComparable
{
    public T recordID;
}
