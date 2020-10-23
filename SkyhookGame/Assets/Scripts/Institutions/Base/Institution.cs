using UnityEngine;

public abstract class Institution : MonoBehaviour
{
    public int Level { get; private set; }

    public InstitutionType InstitutionType { get; private set; }

    public virtual void Upgrade()
    {
        Level++;
    }
}
