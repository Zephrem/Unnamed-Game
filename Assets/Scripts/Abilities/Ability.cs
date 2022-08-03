using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected int abilityId;
    [SerializeField] protected string abilityName;
    [SerializeField] protected int targetRadius;
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

    private void Update()
    {
        //Cancel the coroutine if we click an invalid target or ui element.
        if (Input.GetMouseButtonDown(0) && targetController != null)
        {
            if (targetController.primaryTarget == null)
            {
                EndAbility();
            }
        }
    }

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
            targetController.ClearTargets();

            targetController.onTargetCallback += ExecuteAbility;
        }
    }

    protected abstract void ExecuteAbility();

    protected void EndAbility()
    {
        targetController.onTargetCallback -= ExecuteAbility;
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
        isSelected = state;
    }

    public bool IsSelected()
    {
        return (isSelected);
    }
    #endregion
}
