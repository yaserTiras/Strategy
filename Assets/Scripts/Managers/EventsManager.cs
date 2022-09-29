using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class EventsManager : Singleton<EventsManager>
{
    public delegate void UIRaycaster(GraphicRaycaster raycaster);
    public static event UIRaycaster OnPointerOverUI;

    public delegate void GameStatus();
    public event GameStatus onGameInitialized;
    public event GameStatus onGameStarted;
    public event GameStatus onGameEnded;

    public delegate void UIItemStatus(bool? isPlaced);
    public delegate void ItemClick(Item item);
    public delegate Item ItemHold(Item item);

    public event UIItemStatus onItemClicked;
    public event UIItemStatus onItemHolded;
    public event UIItemStatus onItemReleased;

    public ItemHold holdedUI;
    public ItemClick clickedUI;


    public void MouseOnUI(GraphicRaycaster raycaster)
    {
        OnPointerOverUI(raycaster);
    }

    public void UIItemClicked(Item item)
    {
        clickedUI(item);
    }

    public void ItemReleased(bool isPlaced)
    {
        if (onItemReleased == null)
            return;
        onItemReleased(isPlaced);
    }

    public Item UIItemHolded(Item item)
    {
        if (holdedUI == null)
            return null;

        onItemHolded(false);
        return holdedUI(item);
    }

    public void InitializeGame()
    {
        onGameInitialized();
    }

    public override void Initialize()
    {

    }
}
