using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Ability
{
    protected override void ExecuteAbility(List<Tile> targetList)
    {
        int totalDamage = GetTotalDamage();

        foreach (Tile tile in targetList)
        {
            if(tile.GetUnit() != null)
            {
                tile.GetUnit().GetComponent<EnemyStats>().LoseHealth(totalDamage);
            }
        }

        ConsumeStamina();

        gridController.GridCleanup();
    }
}
