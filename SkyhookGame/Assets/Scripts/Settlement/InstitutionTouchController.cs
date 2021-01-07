using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InstitutionTouchController : MonoBehaviour
{
	[Header("The view (GUI) to display when pressing this institution")]
	[SerializeField] private GameObject preview = default;

	private LayerMask layerMask = default;

	private const string LayerName = "Institution";

    private void Awake()
    {
        if (preview == null)
        {
			Debug.LogWarning($"Initial_view is null on {gameObject.name}");
			enabled = false;
        }

		layerMask = 1 << LayerMask.NameToLayer(LayerName);
    }

    private void Update()
	{
		print(InstitutionsUIManager.Instance.IsUIDisplayed);

		if (preview.activeSelf) return;

		if (Application.isEditor)
		{
			HandleEditorInput();
			return;
        }

		HandleTouchInput();
	}

	private void HandleEditorInput()
	{
		if (!Input.GetMouseButtonDown(0))
			return;

		if (!HandleRaycast(Input.mousePosition))
			return;

		preview.SetActive(true);
	}

	private void HandleTouchInput()
	{
		if (Input.touchCount <= 0) return;

		for (int i = 0; i < Input.touchCount; i++)
		{
			var touch = Input.GetTouch(i);

			if (touch.phase != TouchPhase.Began)
				continue;

			if (!HandleRaycast(touch.position))
				continue;

			preview.SetActive(true);
		}
	}

	private bool HandleRaycast(Vector3 inputPosition)
	{
        var ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit2D[] hits = new RaycastHit2D[1];

        var hitsCount = Physics2D.GetRayIntersectionNonAlloc(ray, hits, 100f, layerMask);

        if (hitsCount == 0)
            return false;

        var hitGameObject = hits[0].collider.gameObject;

        if (hitGameObject.layer != 9)
            return false;

        if (hitGameObject != gameObject)
            return false;

        return true;
    }
}
