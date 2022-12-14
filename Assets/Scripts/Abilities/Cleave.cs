using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleave : Ability
{
    [SerializeField] private int damage;
    [SerializeField] private float falloff;

    protected override void ExecuteAbility(List<Tile> targetList)
    {
        float totalDamage = damage;

        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].GetUnit() != null)
            {
                if (i == 0)
                {
                    targetList[i].GetUnit().GetComponent<EnemyStats>().LoseHealth((int)totalDamage);
                }
                else
                {
                    targetList[i].GetUnit().GetComponent<EnemyStats>().LoseHealth(Mathf.CeilToInt(totalDamage * falloff));
                }
            }
        }

        ConsumeStamina();

        gridController.GridCleanup();
    }
}
