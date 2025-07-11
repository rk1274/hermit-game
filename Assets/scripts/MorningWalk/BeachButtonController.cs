using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachButtonController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer artworkSprite;

    public void UpdateSprite(Sprite iSprite)
    {
        artworkSprite.sprite = iSprite;
    }
}
