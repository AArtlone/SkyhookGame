using MyUtilities.GUI;
using UnityEngine;

public class CosmicPortUIManager : BaseInstitutionUIManager
{
    [Header("View Controllers")]
    [SerializeField] private DocksViewController docksViewController = default;
    [SerializeField] private CosmicPortAssignShipViewController cosmicPortStorageViewController = default;
    [SerializeField] private SendShipViewController sendShipViewController = default;

    public DocksViewController DocksViewController { get { return docksViewController; } }
    public CosmicPortAssignShipViewController CosmicPortStorageViewController { get { return cosmicPortStorageViewController; } }

    protected override void Awake()
    {
        base.Awake();

        docksViewController.gameObject.SetActive(false);
    }

    public override void Btn_UpgradeInstitution()
    {
        Settlement.Instance.CosmicPort.Upgrade();
    }

    public override void Btn_ShowView()
    {
        base.Btn_ShowView();

        preview.SetActive(false);
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


    // For editor reference
    // This Back method overload is need to be able to pass in different navigation controllers
    // This is needed because Manufactory UI makes use of multiple navigation controllers
    public void Back(NavigationController navController)
    {
        navController.Pop();
    }
}
