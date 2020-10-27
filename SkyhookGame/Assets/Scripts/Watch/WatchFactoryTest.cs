using System.Collections.Generic;
using UnityEngine;

public class WatchFactoryTest : MonoBehaviour
{
	private TravelClockFactory travelClockFactory;

	private List<IClock> clocks;

	private void Start()
	{
		clocks = new List<IClock>();

		var watchFactory = new WatchFactory();
		travelClockFactory = watchFactory.CreateTravelFactory();

		clocks.Add(travelClockFactory.CreateExpeditionClock());
		clocks.Add(travelClockFactory.CreateTripClock());
	}

	private void Update()
	{
		foreach (var clock in clocks)
		{
			Debug.Log(clock.GetType().Name + " => " + clock.TimeLeft().ToString());
		}
	}
}