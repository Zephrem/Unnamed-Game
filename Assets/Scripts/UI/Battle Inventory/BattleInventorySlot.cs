using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleInventorySlot : MonoBehaviour
{
    private BattleInventory battleInventory;

    [SerializeField] private string defaultText;

    private Item myItem;
    private TextMeshProUGUI myText;

    private void Awake()
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(CommitItem);
        battleInventory = FindObjectOfType<BattleInventory>();
    }

    private void CommitItem()
    {
        if(myItem != null)
        {
            battleInventory.CommitItem(myItem);
        }
    }

    public void SetUI(Item item)
    {
        myItem = item;

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
