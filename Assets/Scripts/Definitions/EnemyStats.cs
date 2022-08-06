using UnityEngine;

public class EnemyStats : UnitStats
{
    [SerializeField] private Stat itemQuantity;
    [SerializeField] private MetaTable metaTable;
    [SerializeField] private int stageCredit;

    public void LootDrop()
    {
        float finalQuantity = itemQuantity.GetValue();
        float remainder = finalQuantity % 1;

        if(Random.Range(0f, .9f) < remainder)
        {
            finalQuantity += 1;
        }

        for (int i = 0; i < Mathf.Floor(finalQuantity); i++)
        {
            if (metaTable != null)
            {
                FindObjectOfType<BattleInventory>().AddItem(metaTable.RollTable());
            }
        }
    }

    public void Kill()
    {
        GameObject.DestroyImmediate(this.gameObject);
    }

    public int GetCredit()
    {
        return (stageCredit);
    }
}
