using UnityEngine;

public class SiteController : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, 0.6f);
	}
}