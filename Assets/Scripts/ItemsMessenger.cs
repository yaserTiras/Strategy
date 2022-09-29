using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsMessenger : Singleton<ItemsMessenger>
{
    public override void Initialize()
    {
        
    }

    public Item currentProducer;

    public delegate void ItemCreation();
    public event ItemCreation onPoducableItemClicked;

    public void SetProducer(Item item)
    {
        currentProducer = item;
    }

    public bool CanProduce(Item item)
    {
        if (currentProducer == null)
        {
            Debug.Log("Please Select Barracks");
            return false;
        }
        if (currentProducer.Type == item.Producer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ProduceItem(Item itemToProduce)
    {
        if(!currentProducer)
        {
            Debug.Log("Please select producer item");
            return false;
        }

        currentProducer.ProduceItem(itemToProduce);
        return true;        
    }

}
