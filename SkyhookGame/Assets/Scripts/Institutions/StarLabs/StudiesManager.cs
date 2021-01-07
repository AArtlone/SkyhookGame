using MyUtilities;
using System.Collections.Generic;
using UnityEngine;

public class StudiesManager : Singleton<StudiesManager>
{
    [HideInInspector] public List<StudyCode> CompletedStudies = new List<StudyCode>();

    protected override void Awake()
    {
        SetInstance(this);

        CompletedStudies.Add(StudyCode.B);
    }
}
