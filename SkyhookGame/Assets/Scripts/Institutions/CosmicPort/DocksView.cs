﻿using System.Collections.Generic;
using UnityEngine;

public class DocksView : MonoBehaviour
{
    public static DocksView Instance;

    [SerializeField] private List<Dock> allDocks = default;

    [SerializeField] private GameObject buildDockView = default;

    private Dock selectedDock;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        CloseBuildDockView();
    }

    public void UpdateDocksAvailability(int availableDocks)
    {
        if (availableDocks > allDocks.Count)
        {
            Debug.LogError("Number of available docks is more than all docks count. Either reduce the available docks range or add more docks to the All Docks list on " + transform.name);
            enabled = false;
            return;
        }

        for (int i = 0; i < availableDocks; i++)
            allDocks[i].Unlock();
    }

    public void SelectDock(Dock dock)
    {
        if (selectedDock == dock)
            return;

        selectedDock = dock;
    }

    public void BuildDock()
    {
        CloseBuildDockView();

        selectedDock.StartBuilding();
    }

    private void CloseBuildDockView()
    {
        buildDockView.SetActive(false);
    }

    public void ShowBuildDockView()
    {
        buildDockView.SetActive(true);
    }
}