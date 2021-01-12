using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtilities;
using System;

public class ProductionManager : PersistentSingleton<ProductionManager>
{
	/// <summary>
	/// Contains the details for each settlement the player has
	/// </summary>
	private List<SettlementData> settlementsData = new List<SettlementData>();

	private List<ProductionUIManager> productionUIManagers = new List<ProductionUIManager>();

	private float counter = 1;

	protected override void Awake()
	{
		SetInstance(this);
	}

	private void Update()
	{
		counter -= Time.deltaTime;
		if (counter <= 0)
		{
			IncrementResources();
			counter = 1;
		}
	}

	/// <summary>
	/// Retrieves all settlements' data from the PlayerDataManger
	/// </summary>
	public void ImportSettlementsData()
	{
		int totalPlanets = Enum.GetNames(typeof(Planet)).Length;

		for (int i = 0; i < totalPlanets; i++)
		{
			settlementsData.Add(PlayerDataManager.Instance.PlayerData.GetSettlementData((Planet)i));
		}
	}

	/// <summary>
	/// Increments all resources based on the production institute's level
	/// </summary>
	private void IncrementResources()
	{
		foreach (SettlementData settlement in settlementsData)
		{
			foreach (Resource resource in settlement.resources)
			{
				int institutionLevel = settlement.productionData.institutionLevel + 1;
				int valueToAdd = institutionLevel * 5; // TODO: Change it to a non-linear value

				resource.ChangeAmount(valueToAdd);
				RefreshInventories();
			}
		}
	}

	public void AddUIManager(ProductionUIManager manager)
	{
		if (!productionUIManagers.Contains(manager))
		{
			productionUIManagers.Add(manager);
		}
	}

	private void RefreshInventories()
	{
		for (int i = 0; i < productionUIManagers.Count; i++)
		{
			if (!productionUIManagers[i].gameObject.activeSelf) { return; }

			productionUIManagers[i].RefreshInventoryPanels();
		}
	}
}
