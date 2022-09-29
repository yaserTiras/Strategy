using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GraphicRaycaster raycaster;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        raycaster = GetComponent<GraphicRaycaster>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventsManager.instance.MouseOnUI(raycaster);
    }


}
