using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInventory : MonoBehaviour
{
    public delegate void OnItemChangeCallback(Item newItem, Item oldItem);
    public OnItemChangeCallback onItemChangeCallback;

    private List<Item> itemList = new List<Item>();

    public void AddItem(Item newItem)
    {
        itemList.Add(newItem);
        ItemChangeCallback(newItem, null);
    }

    public void CommitItem(Item committedItem)
    {
        Inventory.Instance.AddItem(committedItem);
        ItemChangeCallback(null, committedItem);
    }

    private void ItemChangeCallback(Item newItem, Item oldItem)
    {
        if(onItemChangeCallback != null)
        {
            onItemChangeCallback.Invoke(newItem, oldItem);
        }
    }

    public List<Item> GetItemList()
    {
        return (itemList);
    }
}
