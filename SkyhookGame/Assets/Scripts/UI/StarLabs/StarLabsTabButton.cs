using MyUtilities.GUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLabsTabButton : TabButton
{
    [SerializeField] private StudyType type = default;

    public StudyType Type { get { return type; } }
}
