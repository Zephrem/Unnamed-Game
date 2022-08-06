using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : Ability
{
    [SerializeField] private int damage;

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
                    targetList[i].GetUnit().GetComponent<EnemyStats>().LoseHealth(Mathf.CeilToInt(totalDamage / 2));
                }
            }
        }

        ConsumeStamina();

        gridController.GridCleanup();
    }
}
