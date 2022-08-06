using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignite : Ability
{
    [SerializeField] private int damage;
    [SerializeField] private int duration;

    protected override void ExecuteAbility(List<Tile> targetList)
    {
        for (int i = 0; i < targetList.Count; i++)
        {
            targetList[i].GetUnit().GetComponent<EnemyStats>().LoseHealth(damage);
            targetList[i].GetUnit().GetComponent<EnemyStats>().ApplyDot(damage, duration, DamageOverTime.DotType.Burn);
        }

        ConsumeStamina();

        gridController.GridCleanup();
    }
}
