using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScrollviewController : MonoBehaviour
{

    public List<UIItem> productionElements = new List<UIItem>();

    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform outer;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float maxCheckHeight;
    [SerializeField] private float minCheckHeight;
    [SerializeField] private Padding padding;
    [SerializeField] private float spaceBetweenCells;
    [SerializeField] private float minWidth;
    [SerializeField] private float width;
    [SerializeField] private float height;

    private Vector3 targetPos;

    private float lastDir;
    private void Start()
    {
        maxCheckHeight = outer.position.y + Mathf.Abs(outer.sizeDelta.y) * 0.5f + 200f;
        minCheckHeight = outer.position.y - Mathf.Abs(outer.sizeDelta.y) * 0.5f - 200;
        Setup();
    }

    public float calculatedDim;
    private void Setup()
    {
        width = content.rect.width;
        height = content.rect.height;
        calculatedDim = minWidth;
        float xPos = calculatedDim + spaceBetweenCells;
        float yPos = calculatedDim + spaceBetweenCells;

        for (int i = 0; i < productionElements.Count; i += 2)
        {
            for (int z = 0; z < 2; z++)
            {
                productionElements[i + z].SetSize(calculatedDim, calculatedDim);
                productionElements[i + z].SetAnchoredPosition(new Vector2((z * xPos + calculatedDim * 0.5f) + padding.left, -i * (yPos - calculatedDim * 0.5f) - padding.up));
                productionElements[i + z].name = calculatedDim + "," + calculatedDim;
            }
        }
    }

    private void FixedUpdate()
    {
        if (content.position.y == lastDir)
            return;

        if (content.position.y > lastDir)
        {
            if (content.transform.TransformPoint(content.GetChild(productionElements.Count - 1).localPosition).y > minCheckHeight)
            {
                targetPos = content.GetChild(productionElements.Count - 2).localPosition - (calculatedDim) * Vector3.up;

                content.GetChild(0).SetAsLastSibling();
                content.GetChild(0).SetAsLastSibling();

                content.GetChild(productionElements.Count - 1).localPosition = targetPos + Vector3.right * calculatedDim;
                content.GetChild(productionElements.Count - 2).localPosition = targetPos;
                // Set The First Element To The End
            }
        }
        else
        {
            if (content.transform.TransformPoint(content.GetChild(0).localPosition).y < maxCheckHeight)
            {
                targetPos = content.GetChild(0).localPosition + (calculatedDim) * Vector3.up;

                content.GetChild(productionElements.Count - 1).SetAsFirstSibling();
                content.GetChild(productionElements.Count - 1).SetAsFirstSibling();

                content.GetChild(0).localPosition = targetPos;
                content.GetChild(1).localPosition = targetPos + Vector3.right * calculatedDim;
                // Set The Last Element To The Begining
            }
        }
        lastDir = content.position.y;

    }

    private void ItemHolded(bool? isPlaced)
    {
        scrollRect.vertical = false;
    }

    private void ItemReleased(bool? isPlaced)
    {
        scrollRect.vertical = true;
    }

    private void OnEnable()
    {
        EventsManager.instance.onItemHolded += ItemHolded;
        EventsManager.instance.onItemReleased += ItemReleased;
    }

    private void OnDisable()
    {
        EventsManager.instance.onItemHolded -= ItemHolded;
        EventsManager.instance.onItemReleased -= ItemReleased;
    }


    private void OnValidate()
    {
        //Setup();
    }
}

[Serializable]
public struct Padding
{
    public float left, up;
}
