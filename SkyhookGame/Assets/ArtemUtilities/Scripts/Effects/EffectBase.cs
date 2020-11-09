using System;
using UnityEngine;
[System.Serializable]
public abstract class EffectBase : MonoBehaviour
{
    public bool autoReset = true;
    public bool playOnStart;
    //public bool useCurrentValueAsStart;


    public EffectSOBase effectSO;
    
    private bool playEffect;

    private bool waitingForStartDelay;
    private bool reverse;

    private float normTime;
    private float playTime;

    private float delayTimeLeft;

    protected virtual void Awake()
    {
        if (effectSO.tween.NeedsDelay)
        {
            waitingForStartDelay = true;
            delayTimeLeft = effectSO.tween.delay;
        }

        if (!autoReset)
            return;

        ResetEffect();
    }

    private void OnDisable()
    {
        ResetEffect();
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

            normTime = playTime / effectSO.tween.targetTime;

            if (normTime <= 0)
                EffectFinished();
        }
        else
        {
            playTime += Time.deltaTime;

            normTime = playTime / effectSO.tween.targetTime;

            if (normTime >= 1)
                EffectFinished();
        }
    }

    private void EffectFinished()
    {
        switch (effectSO.tween.playStyle)
        {
            case TweenPlayStyle.Once:
                playEffect = false;
                break;
            case TweenPlayStyle.Repeat:
                ResetEffect();
                break;
            case TweenPlayStyle.PingPong:
                reverse = !reverse;
                break;
        }
    }

    protected virtual void ResetEffect()
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
        return effectSO.tween.curve.Evaluate(normTime);
    }
}
