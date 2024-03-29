﻿using UnityEngine;

public class ShipPrefab : MonoBehaviour
{
    public System.Action<Trip> onLanded;

    [SerializeField] private float timeToTarget = default;

    [Space(5f)]
    [SerializeField] private SpriteRenderer shipSpriteRend = default;
    [SerializeField] private SpriteRenderer boosterSpriteRend = default;
    [SerializeField] private AnimationCurve movementCurve = default;
    [SerializeField] private AnimationCurve landingCurve = default;

    [Space(5f)]
    [SerializeField] private GameObject fireObject = default;

    private ShipFlier shipFlier;

    private Trip tripToEnd;
    private bool landing;

    private void Update()
    {
        HandleRegularFlying();
    }

    private void HandleRegularFlying()
    {
        if (shipFlier == null)
            return;

        if (!shipFlier.Launched)
            return;

        transform.position = new Vector2(transform.position.x, shipFlier.GetNewYValue());

        if (!landing)
            return;

        if (shipFlier.ReachedDestination())
        {
            onLanded?.Invoke(tripToEnd);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Launch(ShipsDSID shipID, int institutionLevel, float targetY)
    {
        fireObject.SetActive(true);

        AssignShipSrite(shipID);

        shipFlier = new ShipFlier(transform.position.y, targetY, timeToTarget, movementCurve);
    }

    public void Land(Trip trip, float targetY)
    {
        tripToEnd = trip;
        landing = true;

        var shipSprite = Resources.Load<Sprite>($"Sprites/Ships/{trip.ship.shipType}");
        shipSpriteRend.sprite = shipSprite;

        boosterSpriteRend.enabled = false;

        shipFlier = new ShipFlier(transform.position.y, targetY, timeToTarget, landingCurve);
    }

    private void AssignShipSrite(ShipsDSID shipID)
    {
        var shipSprite = Resources.Load<Sprite>($"Sprites/Ships/{shipID}");
        shipSpriteRend.sprite = shipSprite;
    }
}