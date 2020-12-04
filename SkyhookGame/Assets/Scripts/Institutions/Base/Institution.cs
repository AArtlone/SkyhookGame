using UnityEngine;

[RequireComponent(typeof(InstitutionTouchController))]
public abstract class Institution : MonoBehaviour
{
    [Header("Institution")]
    [SerializeField] private LevelModule levelModule = default;
    [SerializeField] private SpriteUpgradeModule spriteUpgradeModule = default;

    public LevelModule LevelModule { get { return levelModule; } }

    public InstitutionType InstitutionType { get; private set; }

    public virtual void Upgrade()
    {
        levelModule.IncreaseLevel();

        UpdateVariables();
        DebugVariables();
    }

    protected abstract void InitializeMethod();
    /// <summary>
    /// Updates instituion's variables based on the institution level
    /// </summary>
    protected virtual void UpdateVariables()
    {
        spriteUpgradeModule.SetSprite(levelModule.Level);
    }

    protected abstract void DebugVariables();
}

public class InstitutionData
{
    public int institutionLevel;
}
