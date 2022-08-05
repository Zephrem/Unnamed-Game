using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Ability
{
    [SerializeField] private int damage;

    protected override void ExecuteAbility(List<Tile> targetList)
    {
        foreach (Tile tile in targetList)
        {
            tile.GetUnit().GetComponent<EnemyStats>().LoseHealth(damage);
        }

        ConsumeStamina();

        gridController.GridCleanup();
    }
}
