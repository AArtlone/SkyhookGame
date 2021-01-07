using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InstitutionTouchController : BaseTouchController
{
	[Header("The view (GUI) to display when pressing this institution")]
	[SerializeField] private GameObject preview = default;

	private LayerMask layerMask = default;

	private const string LayerName = "Institution";

    protected override void Awake()
    {
        if (preview == null)
        {
			Debug.LogWarning($"Preview is not assigned in the editor on {gameObject.name} gameobject");
			enabled = false;
        }

        base.Awake();

		layerMask = 1 << LayerMask.NameToLayer(LayerName);
    }

    protected override void Update()
    {
        if (InstitutionsUIManager.Instance.IsUIDisplayed) return;

        if (preview.activeSelf) return;

        if (Application.isEditor)
        {
            HandleEditorInput();
            return;
        }

        HandleTouchInput();
    }

    protected override LayerMask GetLayerMask()
    {
        return layerMask;
    }

    protected override int GetLayerIndex()
    {
        return LayerMask.NameToLayer(LayerName);
    }

    protected override void OnTouch()
    {
        preview.SetActive(true);
    }
}
