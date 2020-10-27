using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image bar = default;

    private bool updateProgressBar;

    private Action callback;
	private TripClock tripClock;
	private TravelClockFactory travelFactory;

	private void Awake()
    {
        ResetBar(true);

		var watchFactory = new WatchFactory();
		travelFactory = watchFactory.CreateTravelFactory();
	}

	private void Update()
	{
		if (!updateProgressBar)
			return;

		if (tripClock.TimeLeft() <= 0)
		{
			updateProgressBar = false;

			gameObject.SetActive(false);

			callback();

			return;
		}

		SetFillAmount(tripClock.ElapsedTime() / tripClock.Duration);
    }

    public void SetFillAmount(float value)
    {
        bar.fillAmount = value;
    }

    public void StartProgressBar(float initialValue, float secondsToWait, Action callback)
    {
        gameObject.SetActive(true);

        this.callback = callback;

        bar.fillAmount = initialValue;

		tripClock = travelFactory.CreateTripClock(secondsToWait);

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
