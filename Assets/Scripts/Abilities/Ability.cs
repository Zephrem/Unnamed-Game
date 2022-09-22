using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public enum TargetingType
    {
        single,
        horizontal,
        vertical,
        cross,
        square
    }

    public enum ScalingStat
    {
        strength,
        intellect
    }

    [SerializeField] protected GameObject spellEffect;
    [SerializeField] protected string abilityName;
    [SerializeField] protected int staminaCost;
    [SerializeField] protected int targetingRange;
    [SerializeField] private TargetingType targetingType;
    [SerializeField] protected int additionalTargets;
    [SerializeField] protected Ability.ScalingStat scalingStat;
    [SerializeField] protected float damageMultiplier;

    protected bool isSelected;

    protected BattleController battleController;
    protected GridController gridController;
    protected TargetController targetController;

    //Abilities are components of the ability index and requires references to be set each time a new battle scene is loaded.
    //Function call is handled by battle controller.
    public void SetReferences()
    {
        if (FindObjectOfType<BattleController>() != null)
        {
            battleController = FindObjectOfType<BattleController>();
            gridController = battleController.GetComponent<GridController>();
            targetController = battleController.GetComponent<TargetController>();
        }
    }

    public void StartAbility()
    {
        if (PlayerStats.Instance.GetStamina() >= staminaCost)
        {
            StartCoroutine(EndAbilityCo());

            targetController.StartTargeting(targetingType, additionalTargets + 1, targetingRange);

            targetController.onTargetChosenCallback += ExecuteAbility;
        }
    }

    protected abstract void ExecuteAbility(List<Tile> targetList);

    protected IEnumerator EndAbilityCo()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        EndAbility();
    }

    protected void EndAbility()
    {
        targetController.StopTargeting();
        targetController.onTargetChosenCallback -= ExecuteAbility;
    }

    protected void ConsumeStamina()
    {
        PlayerStats.Instance.LoseStamina(staminaCost);
    }

    protected void PlayEffect(Vector2 target)
    {
        if(spellEffect != null)
        {
            Instantiate(spellEffect, target, Quaternion.identity);
        }
    }

    protected float GetPlayerStat()
    {
        float stat = 0;

        switch (scalingStat)
        {
            case Ability.ScalingStat.strength:
                stat = PlayerStats.Instance.GetStrength();
                break;
            case Ability.ScalingStat.intellect:
                stat = PlayerStats.Instance.GetIntellect();
                break;
            default:
                break;
        }

        return (stat);
    }

    protected int GetTotalDamage()
    {
        float playerStat = GetPlayerStat();

        int totalDamage = Mathf.RoundToInt(damageMultiplier * playerStat);

        return (totalDamage);
    }

    #region __ACCESSORS__
    public string GetName()
    {
        return (abilityName);
    }

    public void SetSelection(bool state)
    {
        if (state == false)
        {
            isSelected = false;
        }
        else if (AbilityIndex.Instance.GetSelectedSpells() < AbilityIndex.Instance.GetMaxSelectedSpells())
        {
            isSelected = true;
        }
    }

    public bool IsSelected()
    {
        return (isSelected);
    }
    #endregion
}
