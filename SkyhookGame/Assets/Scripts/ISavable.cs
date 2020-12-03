public interface ISavable<T>
{
    T GetSavableData();

    void SetSavableData(T data);
}
