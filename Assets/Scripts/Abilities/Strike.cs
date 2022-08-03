using UnityEngine;

public class Strike : Ability
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
