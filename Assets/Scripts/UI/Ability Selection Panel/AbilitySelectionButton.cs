using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySelectionButton : MonoBehaviour
{
    private Ability myAbility;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ToggleAbility);
    }

    private void ToggleAbility()
    {
        if(myAbility.IsSelected() == false)
        {
            myAbility.SetSelection(true);
        }
        else
        {
            myAbility.SetSelection(false);
        }
    }

    public void SetUI(Ability ability)
    {
        myAbility = ability;
        GetComponentInChildren<TextMeshProUGUI>().text = ability.GetName();
    }
}
