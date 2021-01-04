using UnityEngine;
using UnityEngine.UI;

public class PlanetsOverviewShip : MonoBehaviour
{
    [SerializeField] private Image shipImage = default;

    private ShipFlierV2 shipFlier;

    private Trip trip;

    public void Launch(Trip trip, Vector2 destinationPos)
    {
        this.trip = trip;

        var shipSprite = Resources.Load<Sprite>($"Sprites/Ships/{trip.ship.shipType}");
        shipImage.sprite = shipSprite;

        transform.up = destinationPos;

        shipFlier = new ShipFlierV2(transform.position, destinationPos, trip.timeToDestination, trip.TripClock.TimeLeft());

        trip.onArrived += Trip_OnArrived;
    }

    private void OnDestroy()
    {
        trip.onArrived -= Trip_OnArrived;
    }

    private void Trip_OnArrived()
    {
        trip.onArrived -= Trip_OnArrived;

        Destroy(gameObject);
    }

    private void Update()
    {
        if (!shipFlier.Launched)
            return;

        transform.position = shipFlier.GetNewPos();
    }
}
