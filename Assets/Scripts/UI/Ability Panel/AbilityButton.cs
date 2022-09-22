using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButton : MonoBehaviour
{
    private Ability myAbility;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(UseAbility);
    }

    private void UseAbility()
    {
        AbilityIndex.Instance.ExecuteAbility(myAbility.GetName());
    }

    public void SetUI(Ability ability)
    {
        myAbility = ability;
        GetComponentInChildren<TextMeshProUGUI>().text = myAbility.GetName();
    }
}
