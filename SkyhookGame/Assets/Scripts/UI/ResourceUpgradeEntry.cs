using TMPro;
using UnityEngine;

public class ResourceUpgradeEntry : MonoBehaviour
{
	public ResourcesDSID resourceType;
	[SerializeField] private TextMeshProUGUI currentIncrementLabel;
	[SerializeField] private TextMeshProUGUI newIncrementLabel;

	public void SetCurrentIncrement(int value)
	{
		currentIncrementLabel.text = string.Format($"({value})");
	}

	/// <summary>
	/// Sets the increment change when you level up the production institution
	/// </summary>
	public void SetNewIncrement(int value)
	{
		newIncrementLabel.text = string.Format($"=> ({value})");
		newIncrementLabel.color = Color.green;
	}
}
