using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageOverTime
{
    public enum DotType
    {
        Burn
    }

    public DotType dotType { get; private set; }
    public int damage { get; private set; }
    public int maxDuration { get; private set; }
    public int currentDuration { get; set; }

    public DamageOverTime(int dmg, int maxDur, DamageOverTime.DotType type)
    {
        dotType = type;
        damage = dmg;
        maxDuration = maxDur;
        currentDuration = maxDuration;
    }
}
