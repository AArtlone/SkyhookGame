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
}
