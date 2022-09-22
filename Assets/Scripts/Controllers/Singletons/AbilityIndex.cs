using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIndex : MonoBehaviour
{
    #region __SINGLETON__
    public static AbilityIndex Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    private List<Ability> abilityList;

    [SerializeField] private int maxSelectedSpells;

    void Start()
    {
        abilityList = new List<Ability>();

        PopulateList();
    }

    private void PopulateList()
    {
        foreach(Ability ability in GetComponents<Ability>())
        {
            abilityList.Add(ability);
        }
    }

    public void SetReferences()
    {
        foreach(Ability ability in GameObject.FindObjectOfType<AbilityIndex>().GetComponents<Ability>())
        {
            ability.SetReferences();
        }
    }

    public void ExecuteAbility(string name)
    {
        Ability abilityToUse = abilityList.Find(x => x.GetName() == name);

        if(abilityToUse != null)
        {
            abilityToUse.StartAbility();
        }
    }

    #region __ACCESSORS__
    public List<Ability> GetAbilityList()
    {
        return (abilityList);
    }

    public int GetMaxSelectedSpells()
    {
        return (maxSelectedSpells);
    }

    public int GetSelectedSpells()
    {
        var total = 0;

        for (int i = 0; i < abilityList.Count; i++)
        {
            if (abilityList[i].IsSelected() == true)
            {
                total++;
            }
        }
        return (total);
    }
    #endregion
}
