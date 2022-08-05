using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitStats
{
    public static PlayerStats Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        ResetHealth();
        ResetStamina();
    }

    private new void Start()
    {
        EquippedInventory.Instance.onEquipmentChangeCallback += ApplyModifiers;
    }

    public delegate void OnStaminaChangeCallback(int stamina);
    public OnStaminaChangeCallback onStaminaChangeCallback;

    [SerializeField] private Stat stamina;

    private int currentStamina;

    private void ApplyModifiers(Equipment newEquipment, Equipment oldEquipment)
    {
        List<StatModifier> statList;

        if (oldEquipment != null)
        {
            statList = oldEquipment.GetStatModifiers();

            for (int i = 0; i < statList.Count; i++)
            {
                switch (statList[i].GetTargetStat())
                {
                    case StatModifier.TargetStat.MaxHealth:
                        maxHealth.RemoveMod(statList[i]);
                        break;
                    case StatModifier.TargetStat.Barrier:
                        barrier.RemoveMod(statList[i]);
                        break;
                    case StatModifier.TargetStat.Strength:
                        strength.RemoveMod(statList[i]);
                        break;
                    case StatModifier.TargetStat.Intellect:
                        intellect.RemoveMod(statList[i]);
                        break;
                    case StatModifier.TargetStat.Armor:
                        armor.RemoveMod(statList[i]);
                        break;
                    case StatModifier.TargetStat.Stamina:
                        stamina.RemoveMod(statList[i]);
                        break;
                    default:
                        break;
                }
            }
        }

        if(newEquipment != null)
        {
            statList = newEquipment.GetStatModifiers();

            for (int i = 0; i < statList.Count; i++)
            {
                switch (statList[i].GetTargetStat())
                {
                    case StatModifier.TargetStat.MaxHealth:
                        maxHealth.AddMod(statList[i]);
                        break;
                    case StatModifier.TargetStat.Barrier:
                        barrier.AddMod(statList[i]);
                        break;
                    case StatModifier.TargetStat.Strength:
                        strength.AddMod(statList[i]);
                        break;
                    case StatModifier.TargetStat.Intellect:
                        intellect.AddMod(statList[i]);
                        break;
                    case StatModifier.TargetStat.Armor:
                        armor.AddMod(statList[i]);
                        break;
                    case StatModifier.TargetStat.Stamina:
                        stamina.AddMod(statList[i]);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void GainStamina(int value)
    {
        currentStamina += value;

        StaminaCallback();
    }

    public void LoseStamina(int value)
    {
        currentStamina -= value;

        if(currentStamina < 0)
        {
            currentStamina = 0;
        }

        StaminaCallback();
    }

    public void ResetStamina()
    {
        currentStamina = (int)stamina.GetValue();

        StaminaCallback();
    }

    private void StaminaCallback()
    {
        if (onStaminaChangeCallback != null)
        {
            onStaminaChangeCallback.Invoke(currentStamina);
        }
    }


    #region __ACCESSORS__
    public int GetStamina()
    {
        return (currentStamina);
    }
    #endregion
}
