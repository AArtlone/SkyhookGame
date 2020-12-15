using MyUtilities;
using MyUtilities.GUI;
using UnityEngine;

public class StarLabsUIManager : InstitutionUIManager
{
}

public abstract class InstitutionUIManager : Singleton<InstitutionUIManager>
{
	[Space(10f)]
	[SerializeField] private GameObject preview = default;
	[SerializeField] private GameObject upgradeView = default;

	[Header("Navigation Controllers")]
	[SerializeField] private NavigationController navigationController = default;
	[SerializeField] private StarLabsViewController institutionView = default;

	protected override void Awake()
	{
		preview.SetActive(false);
		upgradeView.SetActive(false);

		SetInstance(this);
	}

	public void Back()
	{
		navigationController.Pop();
	}

	public void Btn_ShowUpgradeView()
    {
		preview.SetActive(false);
		upgradeView.SetActive(true);
	}

	public void Btn_ShowInstitutionView()
	{
		preview.SetActive(false);
		navigationController.Push(institutionView);
	}
}
