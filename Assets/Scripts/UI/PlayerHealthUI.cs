using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    private TextMeshProUGUI textGo;

    private void Start()
    {
        textGo = GetComponent<TextMeshProUGUI>();

        RefreshUI(PlayerStats.Instance.GetHealth());

        PlayerStats.Instance.onHealthChangeCallback += RefreshUI;
    }

    public void RefreshUI(int hp)
    {
        textGo.text = hp.ToString();
    }

    private void OnDestroy()
    {
        PlayerStats.Instance.onHealthChangeCallback -= RefreshUI;
    }
}
