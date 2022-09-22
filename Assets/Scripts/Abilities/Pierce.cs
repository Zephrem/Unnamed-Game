using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : Ability
{
    [SerializeField] private float falloff;

    protected override void ExecuteAbility(List<Tile> targetList)
    {
        int totalDamage = GetTotalDamage();

        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].GetUnit() != null)
            {
                targetList[i].GetUnit().GetComponent<EnemyStats>().LoseHealth(Mathf.RoundToInt(totalDamage * (falloff / (i + 1))));
            }
        }

        ConsumeStamina();

        gridController.GridCleanup();
    }
}
