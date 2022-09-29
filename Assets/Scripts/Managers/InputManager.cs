using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public EventSystem eventSystem;
    public bool isOverUI;


    // UI Essentials
    public GraphicRaycaster graphicRaycaster;
    public PointerEventData pointerData;
    List<RaycastResult> raycastResults = new List<RaycastResult>();

    public float timeToStartDrag = 0.1f;
    private float holdDuration = 0;
    private Item clickedObject;
    private bool startDrag = false;
    public float magnitude;

    private void Awake()
    {
        pointerData = new PointerEventData(eventSystem);
        EventsManager.OnPointerOverUI += SetRaycaster;
    }

    private void Update()
    {
        isOverUI = eventSystem.IsPointerOverGameObject();

        if (isOverUI || startDrag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseDownUI();
            }

            if (Input.GetMouseButton(0))
            {
                if (clickedObject == null)
                    return;
                if (startDrag)
                {
                    HoldItem();
                }
                else
                {
                    HoldTimer();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                MouseUpUI();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseDownInGame(Physics2D.GetRayIntersection(CameraController.instance.mainCamera.ScreenPointToRay(Input.mousePosition)));

            }

            if (Input.GetMouseButtonUp(0))
            {
                MouseUpInGame(Physics2D.GetRayIntersection(CameraController.instance.mainCamera.ScreenPointToRay(Input.mousePosition)));
            }

            // I have wrote this only because the soldier able to move
            if (Input.GetMouseButtonUp(1))
            {

                if (!clickedObject)
                    return;

                if (!clickedObject.CompareTag("Item"))
                    return;

                if (clickedObject.Type != ItemType.Soldier)
                    return;

                RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                if (!rayHit)
                    return;
                if (rayHit.collider.CompareTag("Cell"))
                {
                    clickedObject.Travel(rayHit.transform.GetComponent<Cell>());
                }
            }
        }
    }

    private void HoldItem()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        clickedObject.transform.position = pos;
        clickedObject.OnHold();
    }

    private void MouseDownInGame(RaycastHit2D hit)
    {
        if (!hit)
            return;
        clickedObject = hit.collider.GetComponent<Item>();
    }

    private void MouseUpInGame(RaycastHit2D hit)
    {
        EventsManager.instance.ItemReleased(false);
        if (!hit || !clickedObject)
            return;

        if (hit.collider.CompareTag("Cell"))
        {
            return;
        }

        if (hit.collider.gameObject == clickedObject.gameObject)
        {
            clickedObject.OnClick();
        }
    }

    private void HoldTimer()
    {
        holdDuration += Time.fixedDeltaTime;
        magnitude = Vector3.Distance(clickedObject.transform.position, Input.mousePosition);

        if (magnitude > 20f)
        {
            ResetInput();
        }

        if (holdDuration >= timeToStartDrag)
        {
            clickedObject = EventsManager.instance.UIItemHolded(clickedObject.GetComponent<Item>());

            if (clickedObject)
            {
                magnitude = 0f;
                holdDuration = 0f;
                clickedObject.gameObject.SetActive(true);
                startDrag = clickedObject.IsHoldable;
            }
            else
            {
                return;
            }


        }
    }

    private void ResetInput()
    {
        magnitude = 0f;
        holdDuration = 0f;
        clickedObject = null;
        startDrag = false;
    }

    private void MouseUpUI()
    {
        EventsManager.instance.ItemReleased(clickedObject.IsPlaced);
        if (startDrag)
        {
            startDrag = false;
            clickedObject.OnRelease();
            clickedObject = null;
            return;
        }

        pointerData.position = Input.mousePosition;
        raycastResults.Clear();
        graphicRaycaster.Raycast(pointerData, raycastResults);
        if (raycastResults.Count == 1)
        {
            //Debug.Log(raycastResults[0].gameObject.name);
            raycastResults[0].gameObject.GetComponent<Item>().OnClick();
        }
        else
        {
            holdDuration = 0f;
            clickedObject = null;
            return;
        }
    }

    private void MouseDownUI()
    {
        pointerData.position = Input.mousePosition;
        raycastResults.Clear();
        graphicRaycaster.Raycast(pointerData, raycastResults);
        holdDuration = 0f;
        if (raycastResults.Count == 1)
        {
            clickedObject = raycastResults[0].gameObject.GetComponent<Item>();
        }
        else
        {
            return;
        }
    }


    public void SetRaycaster(GraphicRaycaster raycaster)
    {
        graphicRaycaster = raycaster;
    }
}
