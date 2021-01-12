using TMPro;
using UnityEngine;

public class ResourceEntry : MonoBehaviour
{
	public ResourcesDSID resourceType;
	[SerializeField] private TextMeshProUGUI amountLabel;
	[SerializeField] private TextMeshProUGUI incrementLabel;

	public void SetAmount(int value)
	{
		amountLabel.text = value.ToString();
	}

	/// <summary>
	/// Sets the current increment of the production institution based on its level
	/// </summary>
	public void SetIncrement(int value)
	{
		incrementLabel.text = string.Format($"+({value})");
		incrementLabel.color = Color.green;
	}
}
