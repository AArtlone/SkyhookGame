public interface ISavable<T>
{
    T CreateSaveData();

    void SetSavableData(T data);
}
