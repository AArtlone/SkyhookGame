using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance;

    public static bool Exists { get; private set; }

    protected abstract void Awake();

    protected void SetInstance(T instance)
    {
        Instance = instance;
        Exists = true;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            Exists = false;
        }
    }
}
