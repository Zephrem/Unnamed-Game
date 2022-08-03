using UnityEngine;

public class EnemyStats : UnitStats
{
    [SerializeField] private LootTable lootTable;

    public void LootDrop()
    {
        Inventory.Instance.AddItem(lootTable.GetDrop());
    }

    public void Kill()
    {
        GameObject.DestroyImmediate(this.gameObject);
    }
}
