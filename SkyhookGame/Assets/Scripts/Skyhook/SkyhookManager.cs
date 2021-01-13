using MyUtilities;
using System.Collections.Generic;
using UnityEngine;

public class SkyhookManager : Singleton<SkyhookManager>
{
    [SerializeField] private SkyhookContainer leftContainer = default;
    [SerializeField] private SkyhookContainer rightContainer = default;

    [Space(5f)]
    [SerializeField] private Skyhook skyhookPrefab = default;

    public bool SkyhookIsInstalled { get { return ContainersWithSkyhooks.Count != 0; } }

    public List<SkyhookContainer> ContainersWithSkyhooks { get; private set; } = new List<SkyhookContainer>(2);

    protected override void Awake()
    {
        if (skyhookPrefab == null)
        {
            Debug.LogWarning($"Skyhook prefab is not assigned in the editor on {gameObject.name} gameobject");
            enabled = false;
            return;
        }

        SetInstance(this);
    }

    private void Start()
    {
        StudiesManager.Instance.onInitialized += StudiesManager_OnInit;
        StudiesManager.Instance.onStudyCompleted += StudiesManager_OnStudyCompleted;
    }

    private void StudiesManager_OnInit()
    {
        StudiesManager.Instance.onInitialized -= StudiesManager_OnInit;

        List<StudyCode> completedStudies = StudiesManager.Instance.CompletedStudies;

        foreach (var completedStudy in completedStudies)
        {
            if (completedStudy == StudyCode.AA)
            {
                UnlockSkyhookContainer(leftContainer);
                continue;
            }

            if (completedStudy == StudyCode.AB)
            {
                UnlockSkyhookContainer(rightContainer);
                return;
            }
        }
    }

    private void StudiesManager_OnStudyCompleted(StudyCode studyCode)
    {
        if (studyCode == StudyCode.AA)
        {
            UnlockSkyhookContainer(leftContainer);
            return;
        }

        if (studyCode == StudyCode.AB)
        {
            UnlockSkyhookContainer(rightContainer);
            return;
        }
    }

    public void UnlockSkyhookContainer(SkyhookContainer container)
    {
        container.gameObject.SetActive(true);
    }

    public void SpawnSkyhook(SkyhookContainer container)
    {
        container.EnableSkyhook();
        
        ContainersWithSkyhooks.Add(container);

        Settlement.Instance.Manufactory.RemoveSkyhook();
    }
}
