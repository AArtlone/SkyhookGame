using UnityEngine;

public class CommunityUIManager : BaseInstitutionUIManager
{
    [SerializeField] private CommunityViewController communityViewController = default;

	protected override void Awake()
	{
		base.Awake();

		communityViewController.gameObject.SetActive(false);
	}

	public override void Btn_UpgradeInstitution()
	{
		Settlement.Instance.Community.Upgrade();
	}

	public override void Btn_ShowView()
	{
		base.Btn_ShowView();

		preview.SetActive(false);
		navigationController.Push(communityViewController);
	}
}
