using MyUtilities;
using MyUtilities.GUI;
using UnityEngine;

public class ProductionGUIManager : Singleton<ProductionGUIManager>
{
	[Space(10f)]
	[SerializeField] private GameObject preview = default;
	[SerializeField] private GameObject upgradeView = default;

	[Header("Navigation Controllers")]
	[SerializeField] private NavigationController navigationController = default;
    [SerializeField] private ProductionViewController productionViewController = default;

	protected override void Awake()
	{
		SetInstance(this);

		preview.SetActive(false);
		upgradeView.SetActive(false);
	}

	public void ShowProductionView() 
	{
        navigationController.Push(productionViewController);
	}

	public void Back()
	{
		navigationController.Pop();
	}
}
