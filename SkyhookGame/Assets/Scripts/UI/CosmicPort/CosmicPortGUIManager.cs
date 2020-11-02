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
    public DocksViewController DocksViewController { get { return docksViewController; } }

    protected override void Awake()
    {
        SetInstance(this);

        preview.SetActive(false);
        upgradeView.SetActive(false);
        docksView.gameObject.SetActive(false);
    }

    public void ShowCosmicPortView()
    {
        navigationController.Push(docksViewController);
    }

    public void Back(NavigationController navController)
    {
        navController.PopTopViewController();
    }
}
