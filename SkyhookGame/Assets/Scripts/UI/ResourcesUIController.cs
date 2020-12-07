using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesUIController : MonoBehaviour
{
    [SerializeField] private ResourceUIPrefab prefab = default;
    [SerializeField] private Transform container = default;

    private IEnumerator Start()
    {
        yield return SceneLoader.Instance.WaitForSaveDataApply();

        List<Resource> resources = Settlement.Instance.ResourcesModule.resources;

        foreach (var resource in resources)
        {
            ResourceUIPrefab resourcePrefab = Instantiate(prefab, container);
            var icon = Resources.Load<Sprite>($"UI/Icons/Resources/{resource.ResourceType}");
            resourcePrefab.SetInfo(icon, resource.Amount, resource);
        }
    }
}
