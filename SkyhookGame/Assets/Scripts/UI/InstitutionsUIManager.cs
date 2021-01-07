using MyUtilities;
using UnityEngine;

public class InstitutionsUIManager : Singleton<InstitutionsUIManager>
{
	[SerializeField] private CosmicPortUIManager cosmicPortUIManager = default;
	[SerializeField] private CommunityUIManager communityUIManager = default;
	[SerializeField] private ManufactoryUIManager manufactoryUIManager = default;
	[SerializeField] private ProductionUIManager productionUIManager = default;
	[SerializeField] private StarLabsUIManager starLabsUIManager = default;

	public CosmicPortUIManager CosmicPortUIManager { get { return cosmicPortUIManager; } }
	public CommunityUIManager CommunityUIManager { get { return communityUIManager; } }
	public ManufactoryUIManager ManufactoryUIManager { get { return manufactoryUIManager; } }
	public ProductionUIManager ProductionUIManager { get { return productionUIManager; } }
	public StarLabsUIManager StarLabsUIManager { get { return starLabsUIManager; } }

	public bool IsUIDisplayed { get; private set; }

	protected override void Awake()
	{
		SetInstance(this);
	}

	public void ToggleIsUIDisplayed(bool value)
    {
		IsUIDisplayed = value;
    }
}
