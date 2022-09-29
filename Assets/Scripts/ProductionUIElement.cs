using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionUIElement : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
    }

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

}
