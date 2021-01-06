using UnityEngine;

public class ShipFlier
{
    public bool Launched { get; private set; }

    private float startY;
    private float targetY;
    private float timeToTarget;

    private float normTime;
    private float flyTime;

    private AnimationCurve animationCurve;

    public ShipFlier(float startHeight, float targetHeight, float time, AnimationCurve movementCurve)
    {
        startY = startHeight;
        targetY = targetHeight;

        timeToTarget = time;

        animationCurve = movementCurve;

        Launched = true;
    }

    public float GetNewYValue()
    {
        flyTime += Time.deltaTime;
        normTime = flyTime / timeToTarget;

        float curveValue = GetCurveValue();
        return Mathf.Lerp(startY, targetY, curveValue);
    }

    private float GetCurveValue()
    {
        return animationCurve.Evaluate(normTime);
    }

    public bool ReachedDestination()
    {
        return flyTime >= timeToTarget;
    }
}

public class ShipFlierV2
{
    public bool Launched { get; private set; }

    private Vector2 startPos;
    private Vector2 targetPos;
    private float timeToTarget;

    private float normTime;
    private float flyTime;

    public ShipFlierV2(Vector2 startPos, Vector2 targetPos, float totalTimeToTarget, float timeLeft)
    {
        this.startPos = startPos;
        this.targetPos= targetPos;

        timeToTarget = totalTimeToTarget;

        flyTime = timeToTarget - timeLeft;
        
        Launched = true;
    }

    public Vector2 GetNewPos()
    {
        flyTime += Time.deltaTime;
        normTime = flyTime / timeToTarget;

        return Vector2.Lerp(startPos, targetPos, normTime);
    }
}
