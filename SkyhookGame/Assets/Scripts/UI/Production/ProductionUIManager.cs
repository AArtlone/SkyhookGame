using System.Collections.Generic;
using UnityEngine;

public class ProductionUIManager : BaseInstitutionUIManager
{
    [SerializeField] private ProductionViewController productionViewController = default;

	[SerializeField] private Settlement settlement;
	private SettlementData productionData;

	// field for the current resources inventory panel in the production UI
	[SerializeField] private GameObject inventoryList;
	private List<ResourceEntry> inventoryResources = new List<ResourceEntry>();

	// field for the inventory panel with the new resource upgrades in the production UI
	[SerializeField] private GameObject inventoryUpgradesList;
	private List<ResourceUpgradeEntry> inventoryUpgradesResources = new List<ResourceUpgradeEntry>();

	protected override void Awake()
	{
		base.Awake();

		productionViewController.gameObject.SetActive(false);
	}

	private void Start()
	{
		ProductionManager.Instance.AddUIManager(this);

		productionData = PlayerDataManager.Instance.PlayerData.GetSettlementData(settlement.Planet);

		for (int i = 0; i < inventoryList.transform.childCount; i++)
		{
			inventoryResources.Add(
					inventoryList.transform.GetChild(i).GetComponent<ResourceEntry>()
				);
		}

		for (int i = 0; i < inventoryUpgradesList.transform.childCount; i++)
		{
			inventoryUpgradesResources.Add(
					inventoryUpgradesList.transform.GetChild(i).GetComponent<ResourceUpgradeEntry>()
				);
		}
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

		RefreshInventoryPanel();
	}

	private void RefreshInventoryPanel()
	{
		for (int i = 0; i < inventoryResources.Count; i++)
		{
			ResourceEntry resourceEntry = inventoryResources[i];

			for (int j = 0; j < productionData.resources.Count; j++)
			{
				Resource resource = productionData.resources[j];

				if (resource.ResourceType ==
					resourceEntry.resourceType)
				{
					resourceEntry.SetAmount(resource.Amount);
					resourceEntry.SetIncrement(settlement.Production.LevelModule.Level * 5);
					break;
				}
			}
		}
	}

	private void RefreshInventoryUpgradesPanel()
	{
		for (int i = 0; i < inventoryUpgradesResources.Count; i++)
		{
			ResourceUpgradeEntry resourceUpgradeEntry = inventoryUpgradesResources[i];

			for (int j = 0; j < productionData.resources.Count; j++)
			{
				Resource resource = productionData.resources[j];

				if (resource.ResourceType ==
					resourceUpgradeEntry.resourceType)
				{
					resourceUpgradeEntry.SetCurrentIncrement(
						settlement.Production.LevelModule.Level * 5
					);
					resourceUpgradeEntry.SetNewIncrement(
						(settlement.Production.LevelModule.Level + 1) * 5
					);

					break;
				}
			}
		}
	}

	public void RefreshInventoryPanels()
	{
		RefreshInventoryPanel();
		RefreshInventoryUpgradesPanel();
	}
}