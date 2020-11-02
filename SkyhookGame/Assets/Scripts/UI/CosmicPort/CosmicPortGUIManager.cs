using UnityEngine;

public class CosmicPortGUIManager : Singleton<CosmicPortGUIManager>
{
    [Space(10f)]
    [SerializeField] private GameObject preview = default;
    [SerializeField] private GameObject upgradeView = default;
    [SerializeField] private GameObject docksView = default;

    [Header("Navigation Controllers")]
    [SerializeField] private NavigationController navigationController = default;

    [Header("View Controllers")]
    [SerializeField] private DocksViewController docksViewController = default;
    [SerializeField] private CosmicPortAssignShipViewController cosmicPortStorageViewController = default;
    public DocksViewController DocksViewController { get { return docksViewController; } }
    public CosmicPortAssignShipViewController CosmicPortStorageViewController { get { return cosmicPortStorageViewController; } }

    protected override void Awake()
    {
        SetInstance(this);

        preview.SetActive(false);
        upgradeView.SetActive(false);
        docksView.gameObject.SetActive(false);
    }

    public void ShowCosmicPortView()
    {
        navigationController.Push(docksViewController, true);
    }

    public void ShowCosmicPortAssignShipView()
    {
        navigationController.Push(cosmicPortStorageViewController, false);
    }

    public void Back()
    {
        navigationController.PopTopViewController();
    }

    public void Back(NavigationController navController)
    {
        navController.PopTopViewController();
    }
}
