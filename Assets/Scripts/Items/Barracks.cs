using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Item
{
    private Transform soldiersSpawnPoint;
    public Transform soldiersTargetPoint;
    public Cell producedSoldierTargetCell;
    private Collider2D myCollider;

    protected override void SetUp()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();
        myCollider.enabled = false;
        SpriteRenderer.sprite = Sprite;
        Dimensions = new int[4, 4];
    }

    public override void OnClick()
    {
        if (IsPlaced)
        {
            ItemsMessenger.instance.SetProducer(this);
        }
        else
        {
            // We Can send informations to infromation panel
        }
    }

    public override void ProduceItem(Item producableItem)
    {
        producableItem.OnProduce(hittedCell,producedSoldierTargetCell);
    }


    public override void OnHoldableItemPlaced()
    {
        Cell cell = FieldManager.instance.GetTileAtPosition(hittedCell.Coords.Pos - new Vector2(-1,1));
        if (!cell)
        {
            Debug.Log("Soldiers Spawn Point is out of range");
            return;
        }
        myCollider.enabled = true;
        producedSoldierTargetCell = cell;
    }

    public override void OnHold()
    {
        base.OnHold();
    }
}
