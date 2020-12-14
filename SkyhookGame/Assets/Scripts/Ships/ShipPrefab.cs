using UnityEngine;

public class ShipPrefab : MonoBehaviour
{
    [SerializeField] private float targetY = default;
    [SerializeField] private float timeToTarget = default;

    [Space(5f)]
    [SerializeField] private SpriteRenderer shipSpriteRend = default;
    [SerializeField] private SpriteRenderer boosterSpriteRend = default;
    [SerializeField] private AnimationCurve movementCurve = default;

    private ShipFlier shipFlier;

    private void Update()
    {
        if (!shipFlier.Launched)
            return;

        transform.position = new Vector2(transform.position.x, shipFlier.GetNewYValue());
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Launch(ShipsDSID shipID, int institutionLevel)
    {
        var shipSprite = Resources.Load<Sprite>($"Sprites/Ships/{shipID}");
        shipSpriteRend.sprite = shipSprite;

        var boosterSprite = Resources.Load<Sprite>($"Sprites/Boosters/booster_{institutionLevel - 1}");
        boosterSpriteRend.sprite = boosterSprite;

        shipFlier = new ShipFlier(transform.position.y, targetY, timeToTarget, movementCurve);
    }
}
