using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InstitutionTouchController : MonoBehaviour
{
	/// <summary>
	/// The first panel/view that shows up for this institution
	/// when you tap/click on it to perform actions.
	/// </summary>
	[Header("The view (GUI) to display when pressing this institution")]
	[SerializeField] private GameObject preview = default;

    private void Awake()
    {
        if (preview == null)
        {
			Debug.LogWarning($"Initial_view is null on {gameObject.name}");
			enabled = false;
        }
    }

    private void Update()
	{
		if (preview.activeSelf) return;

		if (Application.isEditor)
		{
			if (!Input.GetMouseButtonDown(0))
				return;

			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D[] hits = new RaycastHit2D[1];
			var hitsCount = Physics2D.GetRayIntersectionNonAlloc(ray, hits, 100f);

			if (hitsCount == 0)
				return;

			var hitGameObject = hits[0].collider.gameObject;

			if (hitGameObject.layer != 9)
				return;

			if (hitGameObject != gameObject)
				return;

			preview.SetActive(true);
			return;
        }

		if (Input.touchCount <= 0) return;

		for (int i = 0; i < Input.touchCount; i++)
		{
			var touch = Input.GetTouch(i);

			if (touch.phase != TouchPhase.Began)
				continue;

			var ray = Camera.main.ScreenPointToRay(touch.position);
			RaycastHit2D[] hits = new RaycastHit2D[1];
			var hitsCount = Physics2D.GetRayIntersectionNonAlloc(ray, hits, 100f);

			if (hitsCount == 0)
				return;

			var hitGameObject = hits[0].collider.gameObject;

			if (hitGameObject.layer != 9)
				return;

			if (hitGameObject != gameObject)
				return;

			preview.SetActive(true);
		}
	}
}
