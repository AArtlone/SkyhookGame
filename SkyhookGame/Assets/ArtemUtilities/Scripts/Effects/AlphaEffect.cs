using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AlphaEffect : Effect<float>
{
    private CanvasGroup canvasGroup;

    protected override void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        
        base.Awake();
    }

    protected override void ApplyEffect()
    {
        canvasGroup.alpha = GetNextValue();
    }

    private float GetNextValue()
    {
        float nextValue = Mathf.Lerp(startValue, targetValue, GetCurveValue());

        return nextValue;
    }

    protected override void Reset()
    {
        base.Reset();

        if (canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup is null, returning");
            return;
        }

        canvasGroup.alpha = startValue;
    }
}
