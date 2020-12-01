using System.Collections;
using System.Collections.Generic;

public class PlayerData
{
    public List<Dock> docksData;

    public PlayerData(List<Dock> docks)
    {
        docksData = docks;
    }
}

public interface ISavable<T>
{
    T GetSavableData();

    void SetSavableData(T data);

    IEnumerator WaitForDataLoadCo();
}
