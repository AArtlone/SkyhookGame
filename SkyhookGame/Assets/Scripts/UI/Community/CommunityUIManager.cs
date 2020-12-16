using MyUtilities;
using MyUtilities.GUI;
using UnityEngine;

public class CommunityUIManager : Singleton<CommunityUIManager>
{
	[Space(10f)]
	[SerializeField] private GameObject preview = default;
	[SerializeField] private GameObject upgradeView = default;

	[Header("Navigation Controllers")]
	[SerializeField] private NavigationController navigationController = default;
    [SerializeField] private CommunityViewController communityViewController = default;

	protected override void Awake()
	{
		SetInstance(this);

		preview.SetActive(false);
		upgradeView.SetActive(false);
		communityViewController.gameObject.SetActive(false);
	}

	public void Btn_ShowCommunityView()
	{
		preview.SetActive(false);
		navigationController.Push(communityViewController);
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
}
