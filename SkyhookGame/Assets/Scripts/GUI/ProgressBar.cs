using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image bar = default;

    private bool updateProgressBar;

    private float timeToWait;
    private float currentWaitTime;

    private Action callback;

    private void Awake()
    {
        ResetBar(true);
    }

    private void Update()
    {
        if (!updateProgressBar)
            return;

        if (currentWaitTime < timeToWait)
        {
            currentWaitTime += Time.deltaTime;

            SetFillAmount(currentWaitTime / timeToWait);
        }
        else
        {
            updateProgressBar = false;

            gameObject.SetActive(false);

            callback();
        }
    }

    public void SetFillAmount(float value)
    {
        bar.fillAmount = value;
    }

    public void StarProgressBar(float initialValue, float secondsToWait, Action callback)
    {
        gameObject.SetActive(true);

        this.callback = callback;

        bar.fillAmount = initialValue;

        timeToWait = secondsToWait;

        updateProgressBar = true;
    }

    private void ResetBar(bool full)
    {
        if (full)
            bar.fillAmount = 1f;
        else
            bar.fillAmount = 0f;
    }
}
