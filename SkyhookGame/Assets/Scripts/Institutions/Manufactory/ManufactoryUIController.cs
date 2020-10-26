using UnityEngine;

public class ManufactoryUIController : MonoBehaviour
{
    [Space(10f)]
    [SerializeField] private GameObject preview = default;
    [SerializeField] private GameObject upgradeView = default;
    [SerializeField] private GameObject manufactoryView = default;
    [SerializeField] private GameObject buildShipView = default;

    private void Awake()
    {
        preview.SetActive(false);
        upgradeView.SetActive(false);
        manufactoryView.gameObject.SetActive(false);
    }

    public void ShowBuildShipView()
    {
        buildShipView.SetActive(true);

        //Pass ship information to the view
    }
}
