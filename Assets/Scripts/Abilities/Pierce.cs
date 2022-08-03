using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : Ability
{
    [SerializeField] private int damage;

    protected override void ExecuteAbility()
    {
        var targetList = targetController.GetTargetList(targetingType, targetRadius);

        foreach (Tile tile in targetList)
        {
            tile.GetUnit().GetComponent<EnemyStats>().LoseHealth(damage);
        }

        EndAbility();
        ConsumeStamina();

        gridController.GridCleanup();
    }
}
