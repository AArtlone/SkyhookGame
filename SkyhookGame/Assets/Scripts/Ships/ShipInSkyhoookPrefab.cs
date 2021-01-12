using UnityEngine;

public class ShipInSkyhoookPrefab : MonoBehaviour
{
    public System.Action<Trip> onLanded;
    
    [Space(5f)]
    [SerializeField] private SpriteRenderer shipSpriteRend = default;

    private ShipFlierV2 shipFlier;

    private Skyhook assignedSkyhook;

    private Trip tripToEnd;

    private Dock launchingDock;
    private Dock destinationDock;

    private bool landing;

    private void Update()
    {
        HandleSkyhookLanding();
    }

    private void HandleSkyhookLanding()
    {
        if (shipFlier == null)
            return;

        if (!shipFlier.Launched)
            return;

        transform.position = shipFlier.GetNewPos();

        if (!landing)
            return;

        if (shipFlier.ReachedDestination())
        {
            onLanded?.Invoke(tripToEnd);
            Destroy(gameObject);
        }
    }

    private void LandFromSkyhook()
    {
        landing = true;

        shipFlier = new ShipFlierV2(transform.position, Settlement.Instance.CosmicPort.transform.position, 2, 2);
    }

    public void WaitForLanding(Skyhook skyhook, Trip trip)
    {
        if (assignedSkyhook != null)
        {
            Debug.LogWarning("Skyhook to launch in is already assigned");
            return;
        }

        tripToEnd = trip;

        AssignShipSrite(trip.ship.shipType);

        assignedSkyhook = skyhook;

        assignedSkyhook.onReachedLandingPoint += Skyhook_OnReachedLandingPoint;
    }

    private void Skyhook_OnReachedLandingPoint()
    {
        assignedSkyhook.onReachedLandingPoint -= Skyhook_OnReachedLandingPoint;

        LandFromSkyhook();
    }

    public void AssignShipSrite(ShipsDSID shipID)
    {
        var shipSprite = Resources.Load<Sprite>($"Sprites/Ships/{shipID}");
        shipSpriteRend.sprite = shipSprite;
    }

    private int GetTimeToDestination(Planet destination)
    {
        return 5;
    }
}
