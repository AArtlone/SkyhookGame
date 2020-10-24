using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image bar = default;

    private void Awake()
    {
        ResetBar(true);
    }

    public void SetFillAmount(float value)
    {
        bar.fillAmount = value;
    }

    private void ResetBar(bool full)
    {
        if (full)
            bar.fillAmount = 1f;
        else
            bar.fillAmount = 0f;
    }
}
