using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaminaUI : MonoBehaviour
{
    private TextMeshProUGUI textGo;

    private void Start()
    {
        textGo = GetComponent<TextMeshProUGUI>();

        RefreshUI(PlayerStats.Instance.GetStamina());

        PlayerStats.Instance.onStaminaChangeCallback += RefreshUI;
    }

    public void RefreshUI(int stamina)
    {
        textGo.text = stamina.ToString();
    }

    private void OnDestroy()
    {
        PlayerStats.Instance.onStaminaChangeCallback -= RefreshUI;
    }
}
