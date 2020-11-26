using MyUtilities;
using MyUtilities.GUI;
using UnityEngine;

public class CommunityGUIManager : Singleton<CommunityGUIManager>
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
	}

	public void ShowCommunityView() 
	{
        navigationController.Push(communityViewController);
	}

	// For editor reference
	public void Back()
	{
		navigationController.Pop();
	}
}
