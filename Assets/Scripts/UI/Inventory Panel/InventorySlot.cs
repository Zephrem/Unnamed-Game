using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    private Item myItem;

    private TextMeshProUGUI myText;

    [SerializeField] private string defaultText;

    private void Awake()
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(UseItem);
    }

    private void UseItem()
    {
        if(myItem != null)
        {
            myItem.Use();
        }
    }

    public void SetUI(Item newItem)
    {
        myItem = newItem;

        if(myItem != null)
        {
            myText.text = myItem.GetName();
        }
        else
        {
            myText.text = defaultText;
        }
    }

    public Item GetItem()
    {
        return (myItem);
    }
}
