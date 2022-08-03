using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipSlot : MonoBehaviour
{
    private Item myItem;
    private Button myButton;
    private TextMeshProUGUI myText;

    [SerializeField] private string defaultText;

    [SerializeField] private Equipment.EquipmentType equipmentType;

    private void Awake()
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(UseEquipment);
    }

    private void UseEquipment()
    {
        if(myItem != null)
        {
            EquippedInventory.Instance.Unequip((int)equipmentType);
        }
    }

    public void SetUI(Item newItem)
    {
        myItem = newItem;

        if(newItem != null)
        {
            myText.text = newItem.GetName();
        }
        else
        {
            myText.text = defaultText;
        }
    }

    public Equipment.EquipmentType GetEquipmentType()
    {
        return (equipmentType);
    }
}
