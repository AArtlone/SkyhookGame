public class TravelClockFactory
{
	public ExpeditionClock CreateExpeditionClock()
	{
		return new ExpeditionClock(10f);
	}

	public TripClock CreateTripClock()
	{
		return new TripClock(20f);
	}
}