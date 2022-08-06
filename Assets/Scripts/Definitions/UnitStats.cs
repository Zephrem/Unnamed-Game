using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public delegate void OnHealthChangeCallback(int hp);
    public OnHealthChangeCallback onHealthChangeCallback;

    public delegate void OnDamagedCallback(int damage);
    public OnDamagedCallback onDamagedCallback;

    [SerializeField] protected Stat maxHealth;
    [SerializeField] protected Stat barrier; //TODO: overshield that restores an amount each turn
    [SerializeField] protected Stat strength; //TODO: gear req. and increase hp
    [SerializeField] protected Stat intellect; //TODO: gear req. and increase barrier
    [SerializeField] protected Stat armor; //TODO: reduce incoming damage

    [SerializeField] protected List<DamageOverTime> dotList = new List<DamageOverTime>();

    protected bool isBurning = false;

    protected int currentHealth;

    protected void Start()
    {
        ResetHealth();
    }

    public void GainHealth(int heal)
    {
        currentHealth += heal;

        HealthCallback();
    }

    public void LoseHealth(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        HealthCallback();
        DamagedCallback(damage);
    }

    public void ApplyDot(int damage, int duration, DamageOverTime.DotType type)
    {
        switch (type)
        {
            case DamageOverTime.DotType.Burn:

                if (isBurning)
                {
                    return;
                }
                else
                {
                    isBurning = true;
                    dotList.Add(new DamageOverTime(damage, duration, type));
                }

                break;

            default:

                break;
        }
    }

    public void ActivateDots()
    {
        for (int i = dotList.Count - 1; i >= 0; i--)
        {
            LoseHealth(dotList[i].damage);
            dotList[i].currentDuration--;

            if (dotList[i].currentDuration <= 0)
            {
                dotList.Remove(dotList[i]);
            }
        }
    }

    protected void HealthCallback()
    {
        if (onHealthChangeCallback != null)
        {
            onHealthChangeCallback.Invoke(currentHealth);
        }
    }

    protected void DamagedCallback(int damage)
    {
        if(onDamagedCallback != null)
        {
            onDamagedCallback.Invoke(damage);
        }
    }

    public void ResetHealth()
    {
        currentHealth = (int)maxHealth.GetValue();

        HealthCallback();
    }

    #region __ACCESSORS__
    public int GetHealth()
    {
        return (currentHealth);
    }

    public int GetMaxHealth()
    {
        return ((int)maxHealth.GetValue());
    }

    public float GetStrength()
    {
        return (strength.GetValue());
    }

    public List<DamageOverTime> GetDotList()
    {
        return (dotList);
    }
    #endregion
}
