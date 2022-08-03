using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedInventory : MonoBehaviour
{
    #region __SINGLETON__
    public static EquippedInventory Instance { get; private set; }
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

    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);
    public OnEquipmentChange onEquipmentChangeCallback;

    [SerializeField] private Equipment[] equippedItems;
    private int numberOfSlots;

    private void Start()
    {
        numberOfSlots = System.Enum.GetNames(typeof(Equipment.EquipmentType)).Length;

        equippedItems = new Equipment[numberOfSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.GetEquipmentType();

        Equipment oldItem = null;

        if (equippedItems[slotIndex] != null)
        {
            oldItem = equippedItems[slotIndex];

            Inventory.Instance.AddItem(oldItem);
        }

        equippedItems[slotIndex] = newItem;

        Inventory.Instance.RemoveItem(newItem);

        EquipmentChangeCallback(newItem, oldItem);
    }

    public void Unequip(int slotIndex)
    {
        Equipment oldItem = null;

        if (equippedItems[slotIndex] != null)
        {
            oldItem = equippedItems[slotIndex];

            Inventory.Instance.AddItem(oldItem);

            equippedItems[slotIndex] = null;
        }

        EquipmentChangeCallback(null, oldItem);
    }

    private void EquipmentChangeCallback(Equipment newItem, Equipment oldItem)
    {
        if (onEquipmentChangeCallback != null)
        {
            onEquipmentChangeCallback.Invoke(newItem, oldItem);
        }
    }

    public Equipment[] GetEquippedItems()
    {
        return (equippedItems);
    }
}
