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
	}

	public void ShowStarLabsView()
	{
		navigationController.Push(starLabsViewController);
	}

	public void Back()
	{
		navigationController.Pop();
	}
}