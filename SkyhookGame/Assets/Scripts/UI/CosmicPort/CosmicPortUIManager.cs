using MyUtilities;
using MyUtilities.GUI;
using UnityEngine;

public class CosmicPortUIManager : Singleton<CosmicPortUIManager>
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

    public void Btn_ShowCosmicPortView()
    {
        preview.SetActive(false);
        navigationController.Push(docksViewController);
    }

    public void Btn_ShowUpgradeView()
    {
        preview.SetActive(false);
        upgradeView.SetActive(true);
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

    public void Btn_UpgradeInstitution()
    {
        Settlement.Instance.CosmicPort.Upgrade();
    }
}
