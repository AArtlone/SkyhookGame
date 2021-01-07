using MyUtilities.GUI;
using UnityEngine;

public class ManufactoryUIManager : BaseInstitutionUIManager
{
    [Header("Navigation Controllers")]
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
        base.Awake();

        manufactoryViewController.gameObject.SetActive(false);
    }

    public override void Btn_UpgradeInstitution()
    {
        Settlement.Instance.Manufactory.Upgrade();
    }

    public override void Btn_ShowView()
    {
        base.Btn_ShowView();

        preview.SetActive(false);
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
    // This Back method overload is need to be able to pass in different navigation controllers
    // This is needed because Manufactory UI makes use of multiple navigation controllers
    public void Back(NavigationController navController)
    {
        navController.Pop();
    }
}
