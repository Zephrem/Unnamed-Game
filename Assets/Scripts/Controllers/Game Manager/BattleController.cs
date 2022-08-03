using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public delegate void OnDamageChangeCallback(int damage);
    public OnDamageChangeCallback onDamageChangeCallback;

    private GridController gridController;

    [SerializeField] private float strengthModifier;

    private int currentStamina;
    private int currentDamage;

    private void Start()
    {
        AbilityIndex.Instance.SetReferences();

        gridController = GetComponent<GridController>();

        gridController.onGridChangeCallback += CalculateDamage;

        PlayerStats.Instance.onStaminaChangeCallback += SetStamina;

        PlayerStats.Instance.ResetHealth();
        PlayerStats.Instance.ResetStamina();

        SetStamina(PlayerStats.Instance.GetStamina());
    }

    private void SetStamina(int stamina)
    {
        currentStamina = stamina;

        if(currentStamina <= 0)
        {
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        PlayerStats.Instance.LoseHealth(currentDamage);

        yield return new WaitForSeconds(.5f);

        if(PlayerStats.Instance.GetHealth() <= 0)
        {
            Debug.Log("Game Over");
        }
        else
        {
            PlayerStats.Instance.ResetStamina();
        }
    }

    public void CalculateDamage()
    {
        float totalStrength = 0;

        foreach(UnitController unit in gridController.unitArray)
        {
            totalStrength += unit.GetComponent<EnemyStats>().GetStrength();
        }

        currentDamage = Mathf.RoundToInt(strengthModifier * totalStrength);

        DamageChangeCallback();
    }

    private void DamageChangeCallback()
    {
        if (onDamageChangeCallback != null)
        {
            onDamageChangeCallback.Invoke(currentDamage);
        }
    }

    private void OnDestroy()
    {
        PlayerStats.Instance.onStaminaChangeCallback -= SetStamina;
    }
}
