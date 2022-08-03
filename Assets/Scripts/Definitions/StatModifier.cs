using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatModifier
{
    public enum ModType
    {
        Flat,
        PercentAdd,
        PercentMulti
    }

    public enum TargetStat
    {
        MaxHealth,
        Barrier,
        Strength,
        Intellect,
        Armor,
        Stamina
    }

    [SerializeField] private float value;
    [SerializeField] private ModType modType;
    [SerializeField] private TargetStat targetStat;

    private int order;

    

    public float GetValue()
    {
        return (value);
    }

    public ModType GetModType()
    {
        return (modType);
    }
    
    public TargetStat GetTargetStat()
    {
        return (targetStat);
    }

    public int GetOrder()
    {
        switch (modType)
        {
            case ModType.PercentAdd:

                order = 1;

                break;

            case ModType.PercentMulti:

                order = 2;

                break;

            default:

                order = 0;

                break;
        }

        return (order);
    }
}
