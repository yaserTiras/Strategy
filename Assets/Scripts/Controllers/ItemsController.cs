using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    [SerializeField] private GameObject barracksPrefab;
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private GameObject powerPlant;

    public Queue<Soldier> soldiers = new Queue<Soldier>();
    public Queue<Barracks> barracks = new Queue<Barracks>();
    public Queue<PowerPlant> powerPlants = new Queue<PowerPlant>();

    private Item item;

    private Item OnProductionItemHold(Item item)
    {
        if (!item.IsHoldable )
            return null;
        switch (item.Type)
        {
            case ItemType.Barracks:
                return barracks.Dequeue();

            case ItemType.Soldier:
                return soldiers.Dequeue();

            case ItemType.PowerPlant:

                return powerPlants.Dequeue();

            default:

                return null;
        }
    }

    private void OnProductionItemClicked(Item item)
    {
        if (!ItemsMessenger.instance.CanProduce(item))
            return;

        switch (item.Type)
        {
            case ItemType.Soldier:
                ItemsMessenger.instance.ProduceItem(soldiers.Dequeue());
                break;
            default:
                break;
        }
    }

    private void Initialize()
    {
        GameObject obj;
        for (int i = 0; i < 10; i++)
        {
            obj = Instantiate(barracksPrefab);
            obj.SetActive(false);
            obj.transform.parent = transform;
            barracks.Enqueue(obj.GetComponent<Barracks>());
        }

        for (int i = 0; i < 16; i++)
        {
            obj = Instantiate(powerPlant);
            obj.SetActive(false);
            obj.transform.parent = transform;
            powerPlants.Enqueue(obj.GetComponent<PowerPlant>());
        }

        for (int i = 0; i < 60; i++)
        {
            obj = Instantiate(soldierPrefab);
            obj.SetActive(false);
            obj.transform.parent = transform;
            soldiers.Enqueue(obj.GetComponent<Soldier>());
        }
    }

    private void OnEnable()
    {
        EventsManager.instance.holdedUI += OnProductionItemHold;
        EventsManager.instance.clickedUI += OnProductionItemClicked;
        EventsManager.instance.onGameInitialized += Initialize;
    }

    private void OnDisable()
    {
        EventsManager.instance.holdedUI -= OnProductionItemHold;
        EventsManager.instance.clickedUI += OnProductionItemClicked;
        EventsManager.instance.onGameInitialized -= Initialize;
    }
}
