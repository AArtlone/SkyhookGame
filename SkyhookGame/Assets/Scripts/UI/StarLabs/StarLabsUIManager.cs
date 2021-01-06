using MyUtilities;
using MyUtilities.GUI;
using UnityEngine;

public class StarLabsUIManager : Singleton<StarLabsUIManager>
{
	[Space(10f)]
	[SerializeField] private GameObject preview = default;
	[SerializeField] private GameObject upgradeView = default;

	[Header("Navigation Controllers")]
	[SerializeField] private NavigationController navigationController = default;
	[SerializeField] private StarLabsViewController starLabsViewController = default;

	protected override void Awake()
	{
		SetInstance(this);

		preview.SetActive(false);
		upgradeView.SetActive(false);
		starLabsViewController.gameObject.SetActive(false);
	}

	public void Btn_ShowStarLabsView()
	{
		preview.SetActive(false);
		navigationController.Push(starLabsViewController);
	}

	public void Btn_ShowUpgradeView()
	{
		preview.SetActive(false);
		upgradeView.SetActive(true);
	}

	public void Back()
	{
		navigationController.Pop();
	}

	public void Btn_UpgradeInstitution()
	{
		Settlement.Instance.StarLabs.Upgrade();
	}
}