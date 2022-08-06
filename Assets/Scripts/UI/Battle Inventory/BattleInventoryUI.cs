using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleInventoryUI : MonoBehaviour
{
    [SerializeField] private Button slotPf;
    [SerializeField] private BattleInventory battleInventory;

    void Start()
    {
        SetUI();
        battleInventory.onItemChangeCallback += RefreshUI;
    }

    private void SetUI()
    {
        for (int i = 0; i < battleInventory.GetItemList().Count; i++)
        {
            AddSlot(battleInventory.GetItemList()[i]);
        }
    }

    private void RefreshUI(Item newItem, Item oldItem)
    {
        if(newItem != null)
        {
            AddSlot(newItem);
        }
        else
        {
            foreach(BattleInventorySlot slot in GetComponentsInChildren<BattleInventorySlot>())
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
        BattleInventorySlot newSlot;
        newSlot = Instantiate(slotPf, this.transform).GetComponent<BattleInventorySlot>();
        newSlot.SetUI(item);
    }

    private void RemoveSlot(BattleInventorySlot slot)
    {
        Destroy(slot.gameObject);
    }
}
