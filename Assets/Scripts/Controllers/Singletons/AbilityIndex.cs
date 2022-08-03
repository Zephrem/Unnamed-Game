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

    // Start is called before the first frame update
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

        abilityList.Sort(SortById);
    }

    private int SortById(Ability ability1, Ability ability2)
    {
        int retval;

        if(ability1.GetId() > ability2.GetId())
        {
            retval = 1;
        }
        else if(ability1.GetId() < ability2.GetId())
        {
            retval = -1;
        }
        else
        {
            retval = 0;
        }

        return (retval);
    }

    public void SetReferences()
    {
        foreach(Ability ability in GameObject.FindObjectOfType<AbilityIndex>().GetComponents<Ability>())
        {
            ability.SetReferences();
        }
    }

    public void ExecuteAbility(int spellId)
    {
        if (spellId < abilityList.Count && abilityList[spellId] != null)
        {
            abilityList[spellId].StartAbility();
        }
    }

    public string GetAbilityName(int spellId)
    {
        if (spellId < abilityList.Count && abilityList[spellId] != null)
        {
            return (abilityList[spellId].GetName());
        }
        else
        {
            return ("Null");
        }
    }

    public List<Ability> GetAbilityList()
    {
        return (abilityList);
    }
}
