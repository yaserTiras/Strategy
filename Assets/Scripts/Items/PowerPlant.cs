using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Item
{
    protected override void SetUp()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = Sprite;
        Dimensions = new int[2, 3];
    }

    public override void OnHold()
    {
        base.OnHold();
    }
}
