using UnityEngine;

public class ProductionUIManager : BaseInstitutionUIManager
{
    [SerializeField] private ProductionViewController productionViewController = default;

	protected override void Awake()
	{
		base.Awake();

		productionViewController.gameObject.SetActive(false);
	}

	public override void Btn_UpgradeInstitution()
	{
		Settlement.Instance.Production.Upgrade();
	}

	public override void Btn_ShowView()
	{
		base.Btn_ShowView();

		preview.SetActive(false);
		navigationController.Push(productionViewController);
	}
}