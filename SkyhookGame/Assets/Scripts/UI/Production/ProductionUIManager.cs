using MyUtilities;
using MyUtilities.GUI;
using UnityEngine;

public class ProductionUIManager : Singleton<ProductionUIManager>
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
		productionViewController.gameObject.SetActive(false);
	}

	public void Btn_ShowProductionView()
	{
		preview.SetActive(false);
		navigationController.Push(productionViewController);
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
