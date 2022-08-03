using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Table/Loot Table", fileName = "New Loot Table")]
public class LootTable : ScriptableObject
{
    [System.Serializable]
    public class Drop
    {
        public Item item;
        public int weight;
    }

    [SerializeField] private List<Drop> itemList = new List<Drop>();

    private int totalWeight = -1;

    private int TotalWeight
    {
        get
        {
            if(totalWeight == -1)
            {
                CalculateTotalWeight();
            }
            return totalWeight;
        }
    }

    private void CalculateTotalWeight()
    {
        totalWeight = 0;

        for (int i = 0; i < itemList.Count; i++)
        {
            totalWeight += itemList[i].weight;
        }
    }

    public Item GetDrop()
    {
        int roll = Random.Range(0, TotalWeight);

        for (int i = 0; i < itemList.Count; i++)
        {
            roll -= itemList[i].weight;

            if (roll < 0)
            {
                return (Instantiate(itemList[i].item));
            }
        }

        return (null);
    }
}
