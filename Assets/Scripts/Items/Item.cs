using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool isPlaced = false;
    [SerializeField] private bool isHoldable = false;
    [SerializeField] private ItemType type;
    [SerializeField] private ItemType producer;
    private int[,] dimensions;
    public Vector3 raycastPosition = Vector3.zero;
    public Cell hittedCell;

    public virtual string Name { get { return itemName; } set { itemName = value; } }
    public virtual Sprite Sprite { get { return sprite; } set { sprite = value; } }
    public virtual SpriteRenderer SpriteRenderer { get { return spriteRenderer; } set { spriteRenderer = value; } }
    public virtual bool IsPlaced { get { return isPlaced; } set { isPlaced = value; } }
    public virtual bool IsHoldable { get { return isHoldable; } set { isHoldable = value; } }
    public virtual ItemType Type { get { return type; } set { type = value; } }
    public virtual ItemType Producer { get { return producer; } set { producer = value; } }
    public virtual int[,] Dimensions { get { return dimensions; } set { dimensions = value; } }

    private void Awake()
    {
        SetUp();
    }

    protected abstract void SetUp();


    public virtual void OnClick()
    {

    }

    public virtual void OnHold()
    {
        CheckHittedCell();
    }

    public virtual void ProduceItem(Item itemToProduce)
    {

    }

    public virtual void OnRelease()
    {
        if (FieldManager.instance.ItemReleased(this))
        {
            transform.position = FieldManager.instance.GetMiddlePosition(this);
            IsPlaced = true;
            OnHoldableItemPlaced();
        }
        else
        {
            FieldManager.instance.ResetField();
            gameObject.SetActive(false);
        }
    }

    public virtual void CheckHittedCell()
    {
        raycastPosition = transform.TransformPoint(SpriteRenderer.sprite.bounds.min);
        raycastPosition.x += 1f;
        raycastPosition.y += 1f;
        RaycastHit2D rayHit = Physics2D.Raycast(raycastPosition, Vector2.up, 1f);

        if (!rayHit)
            return;

        if (hittedCell == rayHit.collider.GetComponent<Cell>())
            return;

        if (rayHit.collider.CompareTag("Cell"))
        {
            hittedCell = rayHit.collider.GetComponent<Cell>();
            FieldManager.instance.CheckIsPlaceble(this);
            return;
        }
    }

    public virtual void OnHoldableItemPlaced()
    {

    }

    public virtual void OnProduce(Cell spawnedCell,Cell targetCell)
    {

    }

    public virtual void Travel(Cell target)
    {

    }

    public virtual void OnEnable()
    {

    }
    
    public virtual void OnDisable()
    {

    }

}
