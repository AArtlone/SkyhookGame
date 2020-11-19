using UnityEngine;

public class CommunityGUIManager : Singleton<CommunityGUIManager>
{
	[Space(10f)]
	[SerializeField] private GameObject preview = default;
	[SerializeField] private GameObject upgradeView = default;
	[SerializeField] private GameObject communityView = default;

	[Header("Navigation Controllers")]
	[SerializeField] private NavigationController navigationController = default;
    [SerializeField] private CommunityViewController communityViewController = default;

	protected override void Awake()
	{
		SetInstance(this);

		preview.SetActive(false);
		upgradeView.SetActive(false);
	}

	public void ShowCommunityView() {
        navigationController.Push(communityViewController);
	}

	// TODO: Perhaps we can move the navigation functions to the base class
	public void PopTopViewController()
	{
		navigationController.Pop();
	}

	// For editor reference
	public void Back(NavigationController navController)
	{
		navController.Pop();
	}
}
