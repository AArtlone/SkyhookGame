using UnityEngine;

public class ManufactoryUIController : Singleton<ManufactoryUIController>
{
    [Space(10f)]
    [SerializeField] private GameObject preview = default;
    [SerializeField] private GameObject upgradeView = default;
    [SerializeField] private GameObject manufactoryView = default;

    [SerializeField] private StorageViewController storageViewController = default;
    public StorageViewController StorageViewController { get { return storageViewController; } }

    protected override void Awake()
    {
        SetInstance(this);

        preview.SetActive(false);
        upgradeView.SetActive(false);
        manufactoryView.gameObject.SetActive(false);
    }
}
