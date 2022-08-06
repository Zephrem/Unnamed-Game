using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleController : MonoBehaviour
{
    public delegate void OnDamageChangeCallback(int damage);
    public OnDamageChangeCallback onDamageChangeCallback;
    private void DamageChangeCallback()
    {
        if (onDamageChangeCallback != null)
        {
            onDamageChangeCallback.Invoke(currentDamage);
        }
    }

    public delegate void OnProgressChangeCallback(int progress);
    public OnProgressChangeCallback onProgressChangeCallback;

    private GridController gridController;

    [SerializeField] private float strengthModifier;
    [SerializeField] private int maxStageProgress;

    private int currentStageProgress;

    [SerializeField] private GameObject endSplash;

    private int currentDamage;

    private void Start()
    {
        AbilityIndex.Instance.SetReferences();

        gridController = GetComponent<GridController>();

        gridController.onGridChangeCallback += CalculateDamage;

        PlayerStats.Instance.onStaminaChangeCallback += CheckStamina;

        PlayerStats.Instance.ResetHealth();
        PlayerStats.Instance.ResetStamina();

        CheckStamina(PlayerStats.Instance.GetStamina());

        currentStageProgress = 0;
    }

    public void AddProgress(int amount)
    {
        currentStageProgress += amount;

        if(onProgressChangeCallback != null)
        {
            onProgressChangeCallback.Invoke(currentStageProgress);
        }
    }

    private void CheckStamina(int stamina)
    {
        if(stamina <= 0)
        {
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        PlayerStats.Instance.LoseHealth(currentDamage);

        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < gridController.GetColumns(); i++)
        {
            for (int j = 0; j < gridController.GetRows(); j++)
            {
                if (gridController.unitArray[i, j] != null)
                {
                    gridController.unitArray[i, j].GetComponent<EnemyStats>().ActivateDots();
                }
            }
        }

        gridController.GridCleanup();

        if (PlayerStats.Instance.GetHealth() <= 0)
        {
            Defeat();
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
            if(unit != null)
            {
                totalStrength += unit.GetComponent<EnemyStats>().GetStrength();
            }
        }

        currentDamage = Mathf.RoundToInt(strengthModifier * totalStrength);

        DamageChangeCallback();
    }

    public void Victory()
    {
        endSplash.SetActive(true);
        endSplash.GetComponentInChildren<TextMeshProUGUI>().text = "Victory!";
    }

    public void Defeat()
    {
        endSplash.SetActive(true);
        endSplash.GetComponentInChildren<TextMeshProUGUI>().text = "Defeat";
    }

    public int GetStageProgress()
    {
        return (currentStageProgress);
    }

    public int GetMaxStageProgress()
    {
        return (maxStageProgress);
    }

    private void OnDestroy()
    {
        PlayerStats.Instance.onStaminaChangeCallback -= CheckStamina;
    }
}
