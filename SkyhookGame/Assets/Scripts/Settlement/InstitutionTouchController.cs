using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InstitutionTouchController : MonoBehaviour
{
	/// <summary>
	/// The first panel/view that shows up for this institution
	/// when you tap/click on it to perform actions.
	/// </summary>
	[Header("The view (GUI) to display when pressing this institution")]
	[SerializeField] private GameObject initial_view;

	private void Update()
	{
		if (initial_view.activeSelf) return;
		if (Input.touchCount <= 0) return;

		for (int i = 0; i < Input.touchCount; i++)
		{
			var touch = Input.GetTouch(i);
			if (touch.phase == TouchPhase.Began)
			{
				var ray = Camera.main.ScreenPointToRay(touch.position);
				var hits = Physics2D.RaycastAll(ray.origin, ray.direction);

				foreach (var hit in hits)
				{
					// layer 9 is for an institution
					if (hit.collider.gameObject.layer == 9)
					{
						initial_view.SetActive(true);
					}
				}
			}
		}
	}
}
