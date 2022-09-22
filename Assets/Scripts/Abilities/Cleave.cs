using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleave : Ability
{
    [SerializeField] private float falloff;

    protected override void ExecuteAbility(List<Tile> targetList)
    {
        int totalDamage = GetTotalDamage();

        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].GetUnit() != null)
            {
                targetList[i].GetUnit().GetComponent<EnemyStats>().LoseHealth(Mathf.RoundToInt(totalDamage * falloff));
            }
        }

        PlayEffect(targetList[0].transform.position);
        ConsumeStamina();

        gridController.GridCleanup();
    }
}
