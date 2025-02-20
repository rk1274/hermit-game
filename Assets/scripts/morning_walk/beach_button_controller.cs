using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beach_button_controller : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer artworkSprite;

    void Start()
    {

    }

    public void UpdateSprite(Sprite iSprite)
    {
        artworkSprite.sprite = iSprite;

    }
}
