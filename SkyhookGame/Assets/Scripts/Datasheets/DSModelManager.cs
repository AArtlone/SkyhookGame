using MyUtilities;
using UnityEngine;

public class DSModelManager : Singleton<DSModelManager>
{
    [SerializeField] private ShipsDSModel shipsModel = default;
    [SerializeField] private ResourcesDSModel resourcesModel = default;
    [SerializeField] private StudiesModel studiesModel = default;

    public ShipsDSModel ShipsModel { get { return shipsModel; } }
    public ResourcesDSModel ResourcesModel { get { return resourcesModel; } }
    public StudiesModel StudiesSO { get { return studiesModel; } }

    protected override void Awake()
    {
        SetInstance(this);
    }
}
