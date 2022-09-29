using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : Item
{

    private RectTransform rectTransform;
    [SerializeField] private Image image;


    public Vector2 GetCellSizeDelta()
    {
        Debug.Log("Ret " + rectTransform.sizeDelta);
        return rectTransform.sizeDelta;
    }

    public Vector2 GetAnchoredPosition()
    {
        return rectTransform.anchoredPosition;
    }

    public void SetAnchoredPosition(Vector2 anchoredPosition)
    {
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    protected override void SetUp()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        image.sprite = Sprite;
    }

    public override void OnClick()
    {
        EventsManager.instance.UIItemClicked(this);
    }

    public override void OnHold()
    {
        EventsManager.instance.UIItemHolded(this);
    }

    public override void OnRelease()
    {
        base.OnRelease();
    }
}
