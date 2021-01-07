using System.Collections.Generic;
using UnityEngine;

public class SkyhookManager : MonoBehaviour
{
    [SerializeField] private List<SkyhookContainer> allContainers = default;

    [SerializeField] private Skyhook skyhookPrefab = default;

    private void Awake()
    {
        if (skyhookPrefab == null)
        {
            Debug.LogWarning($"Skyhook prefab is not assigned in the editor on {gameObject.name} gameobject");
            enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnSkyhook(0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnSkyhook(1);
        }
    }

    public void UnlockSkyhookContainer(int index)
    {
        if (index > allContainers.Count - 1)
        {
            Debug.LogWarning("Index is out of allContainers range");
            return;
        }

        allContainers[index].gameObject.SetActive(true);
    }

    public void SpawnSkyhook(int index)
    {
        if (index > allContainers.Count - 1)
        {
            Debug.LogWarning("Index is out of allContainers range");
            return;
        }

        var container = allContainers[index];

        var skyhook = Instantiate(skyhookPrefab, container.transform);

        skyhook.Initialize(container);

        container.DisableBlueprint();
    }
}
