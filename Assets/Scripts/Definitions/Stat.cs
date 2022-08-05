using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float baseValue;

    private float totalValue;

    private bool isDirty = true;

    private List<StatModifier> modifiers = new List<StatModifier>();

    public void AddMod(StatModifier mod)
    {
        modifiers.Add(mod);
        isDirty = true;
        modifiers.Sort(SortByOrder);
    }

    public void RemoveMod(StatModifier mod)
    {
        modifiers.Remove(mod);
        isDirty = true;
        modifiers.Sort(SortByOrder);
    }

    private int SortByOrder(StatModifier a, StatModifier b)
    {
        if (a.GetOrder() < b.GetOrder())
        {
            return -1;
        }
        else if (a.GetOrder() > b.GetOrder())
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public float GetValue()
    {
        if (isDirty)
        {
            var finalValue = baseValue;
            var totalPercentAdd = 0f;

            for (int i = 0; i < modifiers.Count; i++)
            {
                switch (modifiers[i].GetModType())
                {
                    case StatModifier.ModType.Flat:

                        finalValue += modifiers[i].GetValue();

                        break;

                    case StatModifier.ModType.PercentAdd:

                        totalPercentAdd += modifiers[i].GetValue();

                        if (i + 1 >= modifiers.Count || modifiers[i + 1].GetModType() != StatModifier.ModType.PercentAdd)
                        {
                            finalValue *= 1 + totalPercentAdd;
                        }

                        break;

                    case StatModifier.ModType.PercentMulti:

                        finalValue *= 1 + modifiers[i].GetValue();

                        break;

                    default:

                        break;
                }
            }

            isDirty = false;

            totalValue = finalValue;
        }

        return (totalValue);
    }
}
