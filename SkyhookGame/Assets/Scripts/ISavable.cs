public interface ISavable<T>
{
    T CreatSaveData();

    void SetSavableData(T data);
}
