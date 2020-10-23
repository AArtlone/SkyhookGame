using UnityEngine;

public abstract class Institution : MonoBehaviour
{
    public int Level { get; private set; } = 1;

    public InstitutionType InstitutionType { get; private set; }

    private void Awake()
    {
        InitializeMethod();
    }

    public virtual void Upgrade()
    {
        Level++;
    }

    protected abstract void InitializeMethod();
    protected abstract void UpdateVariables();
    protected abstract void DebugVariables();
}
