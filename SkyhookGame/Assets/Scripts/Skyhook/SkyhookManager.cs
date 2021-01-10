using MyUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyhookManager : Singleton<SkyhookManager>
{
    [SerializeField] private SkyhookContainer leftContainer = default;
    [SerializeField] private SkyhookContainer rightContainer = default;

    [Space(5f)]
    [SerializeField] private Skyhook skyhookPrefab = default;

    public bool SkyhookIsInstalled { get { return InstalledSkyhooks.Count != 0; } }

    public List<Skyhook> InstalledSkyhooks { get; private set; } = new List<Skyhook>(2);

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
    }

    private void StudiesManager_OnInit()
    {
        StudiesManager.Instance.onInitialized -= StudiesManager_OnInit;

        List<StudyCode> completedStudies = StudiesManager.Instance.CompletedStudies;

        foreach (var completedStudy in completedStudies)
        {
            if (completedStudy == StudyCode.B)
            {
                UnlockSkyhookContainer(leftContainer);
                continue;
            }

            if (completedStudy == StudyCode.BA)
            {
                UnlockSkyhookContainer(rightContainer);
                return;
            }
        }
    }

    public void UnlockSkyhookContainer(SkyhookContainer container)
    {
        container.gameObject.SetActive(true);
    }

    public void SpawnSkyhook(SkyhookContainer container)
    {
        var skyhook = Instantiate(skyhookPrefab, container.transform);
        
        InstalledSkyhooks.Add(skyhook);

        skyhook.Initialize(container);

        container.DisableBlueprint();

        Settlement.Instance.Manufactory.RemoveSkyhook();
    }
}
