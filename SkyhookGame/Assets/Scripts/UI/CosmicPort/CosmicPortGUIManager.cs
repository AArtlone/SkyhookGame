using MyUtilities;
using MyUtilities.GUI;
using UnityEngine;

public class CosmicPortGUIManager : Singleton<CosmicPortGUIManager>
{
    [Space(10f)]
    [SerializeField] private GameObject preview = default;
    [SerializeField] private GameObject upgradeView = default;

    [Header("Navigation Controllers")]
    [SerializeField] private NavigationController navigationController = default;

    [Header("View Controllers")]
    [SerializeField] private DocksViewController docksViewController = default;
    [SerializeField] private CosmicPortAssignShipViewController cosmicPortStorageViewController = default;
    [SerializeField] private SendShipViewController sendShipViewController = default;

    public DocksViewController DocksViewController { get { return docksViewController; } }
    public CosmicPortAssignShipViewController CosmicPortStorageViewController { get { return cosmicPortStorageViewController; } }

    protected override void Awake()
    {
        SetInstance(this);

        preview.SetActive(false);
        upgradeView.SetActive(false);

        docksViewController.gameObject.SetActive(false);
    }

    public void ShowCosmicPortView()
    {
        navigationController.Push(docksViewController);
    }

    public void ShowCosmicPortAssignShipView()
    {
        navigationController.Push(cosmicPortStorageViewController);
    }

    public void ShowSendShipView(Dock dock)
    {
        sendShipViewController.AssignDock(dock);

        navigationController.Push(sendShipViewController);
    }

    public void Back()
    {
        navigationController.Pop();
    }

    public void Back(NavigationController navController)
    {
        navController.Pop();
    }
}
