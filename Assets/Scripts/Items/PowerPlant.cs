using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Item
{
    private Collider2D myCollider;
    protected override void SetUp()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();
        myCollider.enabled = false;
        SpriteRenderer.sprite = Sprite;
        Dimensions = new int[2, 3];
    }

    public override void OnHold()
    {
        base.OnHold();
    }

    public override void OnHoldableItemPlaced()
    {
        myCollider.enabled = true;
    }
}
