using UnityEngine;

public class ShipPrefab : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = default;

    private bool launched;
    private float speed = 10f;

    private void Update()
    {
        if (!launched)
            return;

        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    public void SetPrefabInfo(ShipsDSID shipID)
    {
        var sprite = Resources.Load<Sprite>($"Sprites/Ships/{shipID}");
        spriteRenderer.sprite = sprite;
        
        launched = true;
    }
}
