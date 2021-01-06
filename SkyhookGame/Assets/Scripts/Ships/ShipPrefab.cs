using UnityEngine;

public class ShipPrefab : MonoBehaviour
{
    public System.Action<Ship> onLanded;

    //[SerializeField] private float targetY = default;
    //[SerializeField] private float landingTargetY = default;
    [SerializeField] private float timeToTarget = default;

    [Space(5f)]
    [SerializeField] private SpriteRenderer shipSpriteRend = default;
    [SerializeField] private SpriteRenderer boosterSpriteRend = default;
    [SerializeField] private AnimationCurve movementCurve = default;
    [SerializeField] private AnimationCurve landingCurve = default;

    private ShipFlier shipFlier;

    private Ship shipToLand;
    private bool landing;

    private void Update()
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
            onLanded?.Invoke(shipToLand);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Launch(ShipsDSID shipID, int institutionLevel, float targetY)
    {
        var shipSprite = Resources.Load<Sprite>($"Sprites/Ships/{shipID}");
        shipSpriteRend.sprite = shipSprite;

        var boosterSprite = Resources.Load<Sprite>($"Sprites/Boosters/booster_{institutionLevel - 1}");
        boosterSpriteRend.sprite = boosterSprite;

        shipFlier = new ShipFlier(transform.position.y, targetY, timeToTarget, movementCurve);
    }

    public void Land(Ship ship, float targetY)
    {
        shipToLand = ship;
        landing = true;

        var shipSprite = Resources.Load<Sprite>($"Sprites/Ships/{ship.shipType}");
        shipSpriteRend.sprite = shipSprite;

        boosterSpriteRend.enabled = false;

        shipFlier = new ShipFlier(transform.position.y, targetY, timeToTarget, landingCurve);
    }
}
