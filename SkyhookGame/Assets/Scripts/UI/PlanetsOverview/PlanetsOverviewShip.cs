using UnityEngine;
using UnityEngine.UI;

public class PlanetsOverviewShip : MonoBehaviour
{
    [SerializeField] private Image shipImage = default;

    private ShipFlierV2 shipFlier;

    public void Launch(Trip trip, Vector2 destinationPos)
    {
        var shipSprite = Resources.Load<Sprite>($"Sprites/Ships/{trip.ship.shipType}");
        shipImage.sprite = shipSprite;

        transform.up = destinationPos;

        shipFlier = new ShipFlierV2(transform.position, destinationPos, trip.timeToDestination, trip.TripClock.TimeLeft());
    }

    private void Update()
    {
        if (!shipFlier.Launched)
            return;

        transform.position = shipFlier.GetNewPos();
    }
}
