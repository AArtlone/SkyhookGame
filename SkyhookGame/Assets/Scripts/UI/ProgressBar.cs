using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image bar = default;

    private bool updateProgressBar;

    private Action callback;

    private TripClock tripClock;

	private void Awake()
    {
        ResetBar(true);
	}

	private void Update()
	{
		if (!updateProgressBar)
			return;

		if (tripClock.TimeLeft() <= 0)
		{
			updateProgressBar = false;

			gameObject.SetActive(false);

            callback?.Invoke();

            return;
		}

		SetFillAmount(tripClock.ElapsedTime() / tripClock.Duration);
    }

    public void SetFillAmount(float value)
    {
        bar.fillAmount = value;
    }

    public void StartProgressBar(TripClock tripClock, float initialValue)
    {
        this.tripClock = tripClock;

        gameObject.SetActive(true);

        bar.fillAmount = initialValue;

        updateProgressBar = true;
    }

    public void StartProgressBar(TripClock tripClock, float initialValue, Action callback)
    {
        this.tripClock = tripClock;

        gameObject.SetActive(true);

        this.callback = callback;

        bar.fillAmount = initialValue;

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
