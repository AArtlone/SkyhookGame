using UnityEngine;

[RequireComponent(typeof(InstitutionTouchController))]
public abstract class Institution<T> : MonoBehaviour, ISavable<T> where T : InstitutionData
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
        //DebugVariables();
    }

    protected virtual void InitializeMethod()
    {
        var institutionData = GetInstitutionSaveData();

        if (institutionData == null)
        {
            Debug.LogWarning($"Institution data is null");
            SaveDataIsNull();
        }
        else
            SetSavableData(institutionData);

        UpdateVariables();
        //DebugVariables();
    }

    /// <summary>
    /// Override this method to get the T from the PlayerData
    /// </summary>
    /// <returns></returns>
    protected abstract T GetInstitutionSaveData();

    protected virtual void SaveDataIsNull() { }

    public abstract T CreateSaveData();

    public abstract void SetSavableData(T data);

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
