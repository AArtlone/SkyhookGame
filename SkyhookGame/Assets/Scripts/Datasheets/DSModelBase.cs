using System;
using System.Collections.Generic;
using UnityEditor;
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

public abstract class DSModelWindow : EditorWindow
{

}

public class CosmicPortModelWindow : DSModelWindow
{
    public static void ShowWindow()
    {
        GetWindow<CosmicPortModelWindow>();
    }

    //private void OnGUI()
    //{
    //    if (GUILayout.Button("Generate Model"))
    //        GenerateModel();
    //}

    //private void GenerateModel()
    //{
    //    CosmicPortDSModel model = CreateInstance<CosmicPortDSModel>();

    //    AssetDatabase.CreateAsset(model, "Assets/Scripts/Datasheets/CosmicPort/CosmicPortModel.asset");
    //    AssetDatabase.SaveAssets();
    //}
}
