public class WatchFactory
{
	public ConstructionClockFactory CreateConstructionFactory() { return new ConstructionClockFactory(); }
	public ResearchClockFactory CreateResearchFactory() { return new ResearchClockFactory(); }
	public TravelClockFactory CreateTravelFactory() { return new TravelClockFactory(); }
}