using System;
using UnityEngine;

[Serializable]
public class ExperienceModule : LevelModule
{
    [Space(5f)]
    [SerializeField] private int minExp = default;
    [SerializeField] private int maxExp = default;

    public int Experience { get; private set; }

    public void Increase(int value)
    {
        Experience += value;

        int newLevel = animClass.Evaluate(Experience, minExp, maxExp, minLevel, maxLevel);
        
        if (newLevel > Level)
            IncreaseLevel();
    }
}