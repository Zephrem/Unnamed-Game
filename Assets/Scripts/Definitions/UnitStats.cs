using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public delegate void OnHealthChangeCallback(int hp);
    public OnHealthChangeCallback onHealthChangeCallback;

    public delegate void OnDamagedCallaback(int damage);
    public OnDamagedCallaback onDamagedCallback;

    [SerializeField] protected Stat maxHealth;
    [SerializeField] protected Stat barrier; //TODO: overshield that restores an amount each turn
    [SerializeField] protected Stat strength; //TODO: gear req. and increase hp
    [SerializeField] protected Stat intellect; //TODO: gear req. and increase barrier
    [SerializeField] protected Stat armor; //TODO: reduce incoming damage

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
        currentHealth = maxHealth.GetValue();

        HealthCallback();
    }

    #region __ACCESSORS__
    public int GetHealth()
    {
        return (currentHealth);
    }

    public int GetMaxHealth()
    {
        return (maxHealth.GetValue());
    }

    public float GetStrength()
    {
        return (strength.GetValue());
    }
    #endregion
}
