using UnityEngine;

public abstract class EffectBase : MonoBehaviour
{
    public bool autoReset;
    public bool playOnStart;
    public bool useCurrentValueAsStart;
    
    public Tween tween;
    
    private bool playEffect;

    private bool waitingForStartDelay;
    private bool reverse;

    private float normTime;
    private float playTime;

    private float delayTimeLeft;

    protected virtual void Awake()
    {
        if (tween.NeedsDelay)
        {
            waitingForStartDelay = true;
            delayTimeLeft = tween.delay;
        }

        if (!autoReset)
            return;

        Reset();
    }

    private void OnDisable()
    {
        Reset();
    }

    private void Start()
    {
        if (playOnStart)
            PlayEffect();
    }

    private void Update()
    {
        if (!playEffect)
            return;

        if (waitingForStartDelay)
        {
            UpdateDelayTime();
            return;
        }

        UpdateEffectTime();

        ApplyEffect();
    }

    private void UpdateDelayTime()
    {
        delayTimeLeft -= Time.deltaTime;

        if (delayTimeLeft <= 0)
            waitingForStartDelay = false;
    }

    private void UpdateEffectTime()
    {
        if (reverse)
        {
            playTime -= Time.deltaTime;

            normTime = playTime / tween.targetTime;

            if (normTime <= 0)
                EffectFinished();
        }
        else
        {
            playTime += Time.deltaTime;

            normTime = playTime / tween.targetTime;

            if (normTime >= 1)
                EffectFinished();
        }
    }

    private void EffectFinished()
    {
        switch (tween.playStyle)
        {
            case TweenPlayStyle.Once:
                playEffect = false;
                break;
            case TweenPlayStyle.Repeat:
                Reset();
                break;
            case TweenPlayStyle.PingPong:
                reverse = !reverse;
                break;
        }
    }

    protected virtual void Reset()
    {
        playTime = 0f;
    }

    public virtual void PlayEffect()
    {
        playEffect = true;
    }

    protected abstract void ApplyEffect();

    protected float GetCurveValue()
    {
        return tween.curve.Evaluate(normTime);
    }
}
