using System.Diagnostics;

public abstract class IClock
{
	private Stopwatch stopWatch;
	private int duration;

	public IClock(float duration)
	{
		this.duration = (int)duration;
		stopWatch = Stopwatch.StartNew();
	}

	/// <summary>
	/// Returns the time in seconds since the clock started counting.
	/// </summary>
	public int ElapsedTime()
	{
		if (TimeLeft() <= 0)
		{
			stopWatch = null;
			return duration;
		}

		return stopWatch.Elapsed.Seconds;
	}

	public int TimeLeft()
	{
		var timeLeft = duration - stopWatch.Elapsed.Seconds;

		if (timeLeft <= 0)
		{
			stopWatch = null;
			return 0;
		}

		return timeLeft;
	}
}