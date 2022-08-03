using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region __SINGLETON__
    public static Inventory Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public delegate void OnInventoryChange(Item newItem, Item oldItem);
    public OnInventoryChange onInventoryChangeCallback;

    [SerializeField] private List<Item> itemList;
    [SerializeField] private int inventorySize;

    public void AddItem(Item item)
    {
        itemList.Add(item);
        InventoryChangeCallback(item, null);
    }

    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
        InventoryChangeCallback(null, item);
    }

    public List<Item> GetItems()
    {
        return (itemList);
    }

    private void InventoryChangeCallback(Item newItem, Item oldItem)
    {
        if(onInventoryChangeCallback != null)
        {
            onInventoryChangeCallback.Invoke(newItem, oldItem);
        }
    }
}
