using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected int abilityId;
    [SerializeField] protected string abilityName;
    [SerializeField] protected int targetingRadius;
    [SerializeField] protected int targetingRange;
    [SerializeField] protected int staminaCost;

    protected bool isSelected;

    protected GridController gridController;
    protected TargetController targetController;

    public enum TargetingType
    {
        single,
        horizontal,
        vertical,
        cross,
        square
    }

    public TargetingType targetingType;

    public void SetReferences()
    {
        if (FindObjectOfType<GridController>() != null)
        {
            gridController = FindObjectOfType<GridController>();
            targetController = gridController.GetComponent<TargetController>();
        }
    }

    public void StartAbility()
    {
        if (PlayerStats.Instance.GetStamina() >= staminaCost)
        {
            StartCoroutine(EndAbilityCo());

            targetController.StartTargeting(targetingType, targetingRadius, targetingRange);

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

    #region __ACCESSORS__
    public int GetId()
    {
        return (abilityId);
    }

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
