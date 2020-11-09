using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AlphaEffect : EffectBase
{
    private CanvasGroup canvasGroup;

    private FloatEffectSO floatEffectSO;

    protected override void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        floatEffectSO = (FloatEffectSO)effectSO;
        
        base.Awake();
    }

    protected override void ApplyEffect()
    {
        canvasGroup.alpha = GetNextValue();
    }

    private float GetNextValue()
    {
        float nextValue = Mathf.Lerp(floatEffectSO.startValue, floatEffectSO.targetValue, GetCurveValue());

        return nextValue;
    }

    protected override void ResetEffect()
    {
        base.ResetEffect();

        print("reseting");
        if (canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup is null, returning");
            return;
        }

        canvasGroup.alpha = floatEffectSO.startValue;
    }
}
