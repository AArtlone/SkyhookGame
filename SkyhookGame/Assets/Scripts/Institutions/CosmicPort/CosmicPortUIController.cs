using UnityEngine;

public class CosmicPortUIController : MonoBehaviour
{
    [Space(10f)]
    [SerializeField] private GameObject preview = default;
    [SerializeField] private GameObject upgradeView = default;
    [SerializeField] private DocksView docksView = default;

    private void Awake()
    {
        preview.SetActive(false);
        upgradeView.SetActive(false);
        docksView.gameObject.SetActive(false);
    }

    public void ShowDocksView()
    {
        docksView.UpdateDocksAvailability(Settlement.Instance.CosmicPort.AvailableDocks);

        docksView.gameObject.SetActive(true);
    }
}
