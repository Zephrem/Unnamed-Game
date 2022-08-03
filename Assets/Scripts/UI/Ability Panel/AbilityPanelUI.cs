using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject abilityButtonPre;
    [SerializeField] private GameObject buttonHolder;

    void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        AbilityButton button;

        foreach(Ability ability in AbilityIndex.Instance.GetAbilityList())
        {
            if(ability.IsSelected() == true)
            {
                button = Instantiate(abilityButtonPre, buttonHolder.transform).GetComponent<AbilityButton>();
                button.SetUI(ability);
            }
        }
    }
}
