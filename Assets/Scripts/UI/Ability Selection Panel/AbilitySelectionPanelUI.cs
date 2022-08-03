using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelectionPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject abilityButtonPre;
    [SerializeField] private GameObject buttonHolder;

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        AbilitySelectionButton button;

        foreach(Ability ability in AbilityIndex.Instance.GetAbilityList())
        {
            button = Instantiate(abilityButtonPre, buttonHolder.transform).GetComponent<AbilitySelectionButton>();
            button.SetUI(ability);
        }
    }
}
