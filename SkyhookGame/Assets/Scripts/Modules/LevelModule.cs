using System;
using UnityEngine;

[Serializable]
public class LevelModule
{
    [SerializeField] protected AnimationCurveModule animClass;
    
    [Space(5f)]
    [SerializeField] protected int minLevel;
    [SerializeField] protected int maxLevel;

    public int Level { get; private set; } = 1;

    public void IncreaseLevel()
    {
        if (Level >= maxLevel)
            return;

        Level++;
    }

    public int Evaluate(Vector2Int range)
    {
        return animClass.Evaluate(Level, minLevel, maxLevel, range.x, range.y);
    }

    public void SetLevel(int level)
    {
        Level = level;
    }
}
