using UnityEngine;

[RequireComponent(typeof(InstitutionTouchController))]
public abstract class Institution : MonoBehaviour
{
    [SerializeField] private LevelModule levelModule = default;

    public LevelModule LevelModule { get { return levelModule; } }

    public InstitutionType InstitutionType { get; private set; }

    public virtual void Upgrade()
    {
        levelModule.IncreaseLevel();
    }

    protected abstract void InitializeMethod();
    /// <summary>
    /// Updates instituion's variables based on the institution level
    /// </summary>
    protected abstract void UpdateVariables();
    protected abstract void DebugVariables();
}
