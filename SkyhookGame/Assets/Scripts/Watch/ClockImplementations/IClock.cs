using System.Diagnostics;

public abstract class IClock
{
	private Stopwatch stopWatch;
	public float Duration { get; private set; }
	private float timeLeft;
	private float elapsedTime;

	public IClock(float duration)
	{
		this.Duration = duration;
		this.timeLeft = duration;

		stopWatch = Stopwatch.StartNew();
	}

	/// <summary>
	/// Returns the time in milliseconds since the clock started counting.
	/// </summary>
	public float ElapsedTime()
	{
		if (stopWatch != null)
			elapsedTime = stopWatch.Elapsed.Milliseconds * 0.001f + stopWatch.Elapsed.Seconds;

		if (TimeLeft() <= 0)
		{
			stopWatch = null;
			return Duration;
		}

		return elapsedTime;
	}

	/// <summary>
	/// Returns the time left in milliseconds since the clock started counting.
	/// </summary>
	public float TimeLeft()
	{
		if (stopWatch != null)
			timeLeft = Duration - (stopWatch.Elapsed.Milliseconds * 0.001f + stopWatch.Elapsed.Seconds);

		if (timeLeft <= 0)
		{
			stopWatch = null;
			return 0;
		}

		return timeLeft;
	}
}