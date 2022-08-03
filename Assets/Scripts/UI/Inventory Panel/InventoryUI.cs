using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject slotPf;

    // Start is called before the first frame update
    void Start()
    {
        SetUI();
        Inventory.Instance.onInventoryChangeCallback += RefreshUI;
    }

    public void SetUI()
    {
        List<Item> itemList = Inventory.Instance.GetItems();

        for (int i = 0; i < Inventory.Instance.GetItems().Count; i++)
        {
            AddSlot(itemList[i]);
        }
    }

    public void RefreshUI(Item newItem, Item oldItem)
    {
        if(newItem != null)
        {
            AddSlot(newItem);
        }
        else
        {
            foreach(InventorySlot slot in GetComponentsInChildren<InventorySlot>())
            {
                if(slot.GetItem() == oldItem)
                {
                    RemoveSlot(slot);
                }
            }
        }
    }

    private void AddSlot(Item item)
    {
        InventorySlot newSlot;
        newSlot = Instantiate(slotPf, this.transform).GetComponent<InventorySlot>();
        newSlot.SetUI(item);
    }

    private void RemoveSlot(InventorySlot slot)
    {
        Destroy(slot.gameObject);
    }

    private void OnDestroy()
    {
        Inventory.Instance.onInventoryChangeCallback -= RefreshUI;
    }
}
