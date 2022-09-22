using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private Slider hpSlider;

    private void Start()
    {
        hpSlider = GetComponent<Slider>();

        hpSlider.maxValue = PlayerStats.Instance.GetMaxHealth();

        RefreshUI(PlayerStats.Instance.GetHealth());

        PlayerStats.Instance.onHealthChangeCallback += RefreshUI;
    }

    public void RefreshUI(int hp)
    {
        hpSlider.value = hp;
    }

    private void OnDestroy()
    {
        PlayerStats.Instance.onHealthChangeCallback -= RefreshUI;
    }
}
