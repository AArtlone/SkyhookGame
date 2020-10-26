using UnityEngine;

public class ScaleEffect : Effect<Vector3>
{
    protected override void ApplyEffect()
    {
        transform.localScale = GetNextValue();
    }

    private Vector3 GetNextValue()
    {
        Vector3 nextValue = Vector3.Lerp(startValue, targetValue, GetCurveValue());

        return nextValue;
    }

    protected override void Reset()
    {
        base.Reset();
        
        transform.localScale = startValue;
    }
}
