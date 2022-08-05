using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Table/Meta Table", fileName = "New Meta Table")]
public class MetaTable : ScriptableObject
{
    [System.Serializable]
    public class Drop
    {
        public LootTable table;
        public int weight;
    }

    [SerializeField] private List<Drop> tableList = new List<Drop>();

    private int totalWeight = -1;

    private int TotalWeight
    {
        get
        {
            if (totalWeight == -1)
            {
                CalculateTotalWeight();
            }
            return totalWeight;
        }
    }

    private void CalculateTotalWeight()
    {
        totalWeight = 0;

        for (int i = 0; i < tableList.Count; i++)
        {
            totalWeight += tableList[i].weight;
        }
    }

    public Item RollTable()
    {
        int roll = Random.Range(0, TotalWeight);

        for (int i = 0; i < tableList.Count; i++)
        {
            roll -= tableList[i].weight;

            if (roll < 0)
            {
                return(tableList[i].table.GetDrop());
            }
        }

        return (null);
    }
}
