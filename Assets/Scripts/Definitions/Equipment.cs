using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment", fileName = "New Equipment")]
public class Equipment : Item
{
    public enum EquipmentType
    {
        Weapon,
        Armor,
        Trinket
    }

    [SerializeField] private EquipmentType equipmentType;

    [SerializeField] private List<StatModifier> statMods = new List<StatModifier>();

    public override void Use()
    {
        EquippedInventory.Instance.Equip(this);
    }

    public List<StatModifier> GetStatModifiers()
    {
        return (statMods);
    }

    public EquipmentType GetEquipmentType()
    {
        return (equipmentType);
    }
}
