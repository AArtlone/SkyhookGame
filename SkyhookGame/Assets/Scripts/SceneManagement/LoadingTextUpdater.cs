using TMPro;
using UnityEngine;

public class LoadingTextUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText = default;

    private void Update()
    {
        string text = "Loading - " + MySceneManager.Instance.LoadingOperationProgress.ToString() + "%";

        loadingText.text = text;
    }
}
