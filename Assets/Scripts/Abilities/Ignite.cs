using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignite : Ability
{
    [SerializeField] private int duration;

    protected override void ExecuteAbility(List<Tile> targetList)
    {
        int totalDamage = GetTotalDamage();

        for (int i = 0; i < targetList.Count; i++)
        {
            targetList[i].GetUnit().GetComponent<EnemyStats>().LoseHealth(totalDamage);
            targetList[i].GetUnit().GetComponent<EnemyStats>().ApplyDot(totalDamage, duration, DamageOverTime.DotType.Burn);
        }

        ConsumeStamina();

        gridController.GridCleanup();
    }
}
