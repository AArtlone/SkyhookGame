public class TravelClockFactory
{
	public ExpeditionClock CreateExpeditionClock(float duration)
	{
		return new ExpeditionClock(duration);
	}

	public TripClock CreateTripClock(float duration)
	{
		return new TripClock(duration);
	}
}