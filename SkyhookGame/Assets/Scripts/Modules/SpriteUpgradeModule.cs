using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpriteUpgradeModule
{
    [Tooltip("Sprites must be in the correct order")]
    [SerializeField] private List<Sprite> allSprites = default;

    [Space(5f)]
    [SerializeField] private SpriteRenderer spriteRenderer = default;

    public void SetSprite(int level)
    {
        int index = level - 1;

        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer is null");
            return;
        }

        if (index >= allSprites.Count)
        {
            Debug.LogWarning("Index is outside of the list range");
            return;
        }

        spriteRenderer.sprite = allSprites[index];
    }
}
