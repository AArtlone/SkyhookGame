using UnityEngine;

public class CosmicPortUIController : Singleton<CosmicPortUIController>
{
    [Space(10f)]
    [SerializeField] private GameObject preview = default;
    [SerializeField] private GameObject upgradeView = default;
    [SerializeField] private GameObject docksView = default;

    [SerializeField] private DocksViewController docksViewController = default;
    public DocksViewController DocksViewController { get { return docksViewController; } }

    protected override void Awake()
    {
        SetInstance(this);

        preview.SetActive(false);
        upgradeView.SetActive(false);
        docksView.gameObject.SetActive(false);
    }
}
