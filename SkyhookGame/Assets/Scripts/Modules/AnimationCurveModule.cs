using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "Create Animation Curve SO")]
public class AnimationCurveModule : ScriptableObject
{
    public AnimationCurve AnimationCurve;

    public int Evaluate(int currentXValue, int minXValue, int maxXValue, int minYValue, int maxYValue)
    {
        float norm = ((float)currentXValue - minXValue) / (maxXValue - minXValue);

        float curveValue = AnimationCurve.Evaluate(norm);

        int result = (int)Mathf.Lerp(minYValue, maxYValue, curveValue);

        return result;
    }
}
