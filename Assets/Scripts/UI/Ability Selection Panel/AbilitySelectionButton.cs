using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySelectionButton : MonoBehaviour
{
    private Ability myAbility;

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color selectedColor;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ToggleAbility);

        ToggleHighlight();
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

        ToggleHighlight();
    }

    private void ToggleHighlight()
    {
        if (myAbility.IsSelected() == true)
        {
            GetComponent<Image>().color = selectedColor;
        }
        else
        {
            GetComponent<Image>().color = defaultColor;
        }
    }

    public void SetUI(Ability ability)
    {
        myAbility = ability;
        GetComponentInChildren<TextMeshProUGUI>().text = ability.GetName();
        ToggleHighlight();
    }
}
