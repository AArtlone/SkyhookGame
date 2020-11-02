using UnityEngine;

public class ManufactoryGUIManager : Singleton<ManufactoryGUIManager>
{
    [Space(10f)]
    [SerializeField] private GameObject preview = default;
    [SerializeField] private GameObject upgradeView = default;
    [SerializeField] private GameObject manufactoryView = default;

    [Header("Navigation Controllers")]
    [SerializeField] private NavigationController navigationController = default;
    [SerializeField] private NavigationController tabsNavigationController = default;

    [Header("View Controllers")]
    [SerializeField] private ManufactoryViewController manufactoryViewController = default;
    [SerializeField] private StorageViewController storageViewController = default;
    [SerializeField] private ManufactoryTasksViewController tasksViewController = default;
    [SerializeField] private AssignShipToDockViewController assignShipToDock = default;

    public StorageViewController StorageViewController { get { return storageViewController; } }
    public ManufactoryTasksViewController TasksViewController { get { return tasksViewController; } }
    public AssignShipToDockViewController AssignShipToDock { get { return assignShipToDock; } }

    protected override void Awake()
    {
        SetInstance(this);

        preview.SetActive(false);
        upgradeView.SetActive(false);
        manufactoryView.gameObject.SetActive(false);
    }

    public void ShowManufactoryView()
    {
        navigationController.Push(manufactoryViewController);
    }

    public void DisplayTabPage(ViewController viewController)
    {
        tabsNavigationController.PushAndPop(viewController);
    }

    public void Back(NavigationController navController)
    {
        navController.PopTopViewController();
    }
}
