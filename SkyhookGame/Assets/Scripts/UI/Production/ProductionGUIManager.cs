using MyUtilities;
using MyUtilities.GUI;
using UnityEngine;

public class ProductionGUIManager : Singleton<ProductionGUIManager>
{
	[Space(10f)]
	[SerializeField] private GameObject preview = default;
	[SerializeField] private GameObject upgradeView = default;
	[SerializeField] private GameObject productionView = default;

	[Header("Navigation Controllers")]
	[SerializeField] private NavigationController navigationController = default;
    [SerializeField] private ProductionViewController productionViewController = default;

	protected override void Awake()
	{
		SetInstance(this);

		preview.SetActive(false);
		upgradeView.SetActive(false);
	}

	public void ShowProductionView() {
        navigationController.Push(productionViewController);
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
