using MyUtilities;
using UnityEngine;

public class DSModelManager : Singleton<DSModelManager>
{
    [SerializeField] private ShipsDSModel shipsModel = default;

    public ShipsDSModel ShipsModel { get { return shipsModel; } }

    protected override void Awake()
    {
        SetInstance(this);
    }
}
