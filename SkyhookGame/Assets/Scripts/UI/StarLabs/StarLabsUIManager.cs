using UnityEngine;

public class StarLabsUIManager : BaseInstitutionUIManager
{
	[SerializeField] private StarLabsViewController starLabsViewController = default;

	protected override void Awake()
	{
		base.Awake();

		starLabsViewController.gameObject.SetActive(false);
	}

	public override void Btn_UpgradeInstitution()
	{
		Settlement.Instance.StarLabs.Upgrade();
	}

	public override void Btn_ShowView()
	{
		base.Btn_ShowView();

		preview.SetActive(false);
		navigationController.Push(starLabsViewController);
	}
}