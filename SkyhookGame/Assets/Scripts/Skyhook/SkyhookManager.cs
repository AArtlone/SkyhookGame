using MyUtilities;
using UnityEngine;

public class SkyhookManager : Singleton<SkyhookManager>
{
    [SerializeField] private Skyhook skyhookPrefab = default;

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

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    SpawnSkyhook(0);
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    SpawnSkyhook(1);
        //}
    }

    public void UnlockSkyhookContainer(SkyhookContainer container)
    {
        container.gameObject.SetActive(true);
    }

    public void SpawnSkyhook(SkyhookContainer container)
    {
        var skyhook = Instantiate(skyhookPrefab, container.transform);

        skyhook.Initialize(container);

        container.DisableBlueprint();

        Settlement.Instance.Manufactory.RemoveSkyhook();
    }
}
