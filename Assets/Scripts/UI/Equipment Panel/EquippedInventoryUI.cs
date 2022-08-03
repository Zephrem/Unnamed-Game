using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedInventoryUI : MonoBehaviour
{
    private EquipSlot[] equipSlots;

    // Start is called before the first frame update
    void Start()
    {
        equipSlots = GetComponentsInChildren<EquipSlot>();

        SetUI();

        EquippedInventory.Instance.onEquipmentChangeCallback += RefreshUI;
    }

    private void SetUI()
    {
        Equipment[] equippedItems = EquippedInventory.Instance.GetEquippedItems();

        for (int i = 0; i < equippedItems.Length; i++)
        {
            for (int j = 0; j < equipSlots.Length; j++)
            {
                if (i == (int)equipSlots[j].GetEquipmentType())
                {
                    if (equippedItems[i] == null)
                    {
                        equipSlots[j].SetUI(null);
                    }
                    else
                    {
                        equipSlots[j].SetUI(equippedItems[i]);
                    }
                }
            }
        }
    }

    private void RefreshUI(Equipment newItem, Equipment oldItem)
    {
        foreach (EquipSlot slot in equipSlots)
        {
            if(newItem != null)
            {
                if(slot.GetEquipmentType() == newItem.GetEquipmentType())
                {
                    slot.SetUI(newItem);
                    return;
                }
            }
            else
            {
                if(slot.GetEquipmentType() == oldItem.GetEquipmentType())
                {
                    slot.SetUI(null);
                    return;
                }
            }
        }
    }

    private void OnDestroy()
    {
        EquippedInventory.Instance.onEquipmentChangeCallback -= RefreshUI;
    }
}
