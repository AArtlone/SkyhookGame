using UnityEngine;

public abstract class LevelUpSOBase : ScriptableObject
{
    public AnimationCurve animationCurve = default;

    protected int Evaluate(int currentXValue, int minXValue, int maxXValue, int minYValue, int maxYValue)
    {
        float norm = ((float)currentXValue - minXValue) / (maxXValue - minXValue);

        float curveValue = animationCurve.Evaluate(norm);

        int result = (int)Mathf.Lerp(minYValue, maxYValue, curveValue);

        return result;
    }
}
