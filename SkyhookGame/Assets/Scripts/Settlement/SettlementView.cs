﻿using UnityEngine;

public class SettlementView : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		for (int i = 0; i < 3; i++)
		{
			Gizmos.DrawWireSphere(transform.position, (float)((i + 1.3 + 3) * 1.7));
		}
	}
}