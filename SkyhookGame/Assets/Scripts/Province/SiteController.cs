using UnityEngine;

public class SiteController : MonoBehaviour
{
	public SiteType siteType;

	private void OnDrawGizmos()
	{
		switch (siteType)
		{
			case SiteType.Circumsolar:
				Gizmos.color = Color.cyan;
				break;
			case SiteType.Satellite:
				Gizmos.color = Color.magenta;
				break;
			case SiteType.Surface:
				Gizmos.color = Color.blue;
				break;
			default:
				break;
		}

		Gizmos.DrawWireSphere(transform.position, 0.6f);
	}
}