using MyUtilities;
using UnityEngine;

public class DSModelManager : Singleton<DSModelManager>
{
    [SerializeField] private ShipsDSModel shipsModel = default;
    [SerializeField] private ResourcesDSModel resourcesModel = default;

    public ShipsDSModel ShipsModel { get { return shipsModel; } }
    public ResourcesDSModel ResourcesModel { get { return resourcesModel; } }

    protected override void Awake()
    {
        SetInstance(this);
    }
}
