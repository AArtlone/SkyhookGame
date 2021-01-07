using MyUtilities;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : Singleton<SceneLoader>
{
    List<MonoBehaviour> allWaiters = new List<MonoBehaviour>();
    List<MonoBehaviour> saveDataWaiters = new List<MonoBehaviour>();

    protected override void Awake()
    {
        SetInstance(this);
    }

    public WaitUntil WaitForLoading()
    {
        return new WaitUntil(() =>
        {
            print("waiting: " + allWaiters.Count);

            if (allWaiters.Count == 0)
                return true;
            else
                return false;
        });
    }

    public WaitUntil WaitForSaveDataApply()
    {
        return new WaitUntil(() =>
        {
            print("waiting: " + saveDataWaiters.Count);

            if (saveDataWaiters.Count == 0)
                return true;
            else
                return false;
        });
    }

    public void AddWaiter(MonoBehaviour mb)
    {
        if (!allWaiters.Contains(mb))
            allWaiters.Add(mb);
    }

    public void RemoveWaiter(MonoBehaviour mb)
    {
        if (allWaiters.Contains(mb))
            allWaiters.Remove(mb);
    }

    public void AddSaveDataWaiter(MonoBehaviour mb)
    {
        if (!saveDataWaiters.Contains(mb))
            saveDataWaiters.Add(mb);
    }

    public void RemoveSaveDataWaiter(MonoBehaviour mb)
    {
        if (saveDataWaiters.Contains(mb))
            saveDataWaiters.Remove(mb);
    }
}
