using UnityEngine;

public class ShipPrefab : MonoBehaviour
{
    [SerializeField] private float targetY = default;
    [SerializeField] private float timeToTarget = default;

    [Space(5f)]
    [SerializeField] private SpriteRenderer spriteRenderer = default;
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

    public void Launch(ShipsDSID shipID)
    {
        var sprite = Resources.Load<Sprite>($"Sprites/Ships/{shipID}");
        spriteRenderer.sprite = sprite;

        shipFlier = new ShipFlier(transform.position.y, targetY, timeToTarget, movementCurve);
    }
}
