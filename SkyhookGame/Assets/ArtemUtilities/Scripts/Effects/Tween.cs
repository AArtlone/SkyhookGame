using System;
using UnityEngine;

[Serializable]
public class Tween
{
    public TweenPlayStyle playStyle;
    public AnimationCurve curve;
    public float targetTime;
    public float delay;

    public bool NeedsDelay { get { return delay != 0; } }
}
