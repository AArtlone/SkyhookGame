using MyUtilities;
using MyUtilities.GUI;
using UnityEngine;

public class ManufactoryGUIManager : Singleton<ManufactoryGUIManager>
{
    [Space(10f)]
    [SerializeField] private GameObject preview = default;
    [SerializeField] private GameObject upgradeView = default;
    [SerializeField] private GameObject manufactoryView = default;

    [Header("Navigation Controllers")]
    [SerializeField] private NavigationController navigationController = default;
    [SerializeField] private NavigationController tabsNavigationController = default; //Navigation controller that is responsible for handling views of the TabGroup

    [Header("View Controllers")]
    [SerializeField] private ManufactoryViewController manufactoryViewController = default;
    [SerializeField] private FactoryViewController factoryViewController = default;
    [SerializeField] private StorageViewController storageViewController = default;
    [SerializeField] private ManufactoryTasksViewController tasksViewController = default;
    [SerializeField] private AssignShipToDockViewController assignShipToDockView = default;

    public FactoryViewController FactoryViewController { get { return factoryViewController; } }
    public StorageViewController StorageViewController { get { return storageViewController; } }
    public ManufactoryTasksViewController TasksViewController { get { return tasksViewController; } }
    public AssignShipToDockViewController AssignShipToDock { get { return assignShipToDockView; } }

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

    public void ShowAssignShipToDockView()
    {
        navigationController.Push(assignShipToDockView);
    }

    public void PopTopViewController()
    {
        navigationController.Pop();
    }

    public void ShowTabPage(ViewController tabPageToDIsplay)
    {
        tabsNavigationController.PushAndPop(tabPageToDIsplay);
    }

    public void HideTabPage()
    {
        tabsNavigationController.Pop();
    }

    // For editor reference
    public void Back(NavigationController navController)
    {
        navController.Pop();
    }
}
