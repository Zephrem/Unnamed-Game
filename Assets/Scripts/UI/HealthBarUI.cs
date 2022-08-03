using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private EnemyStats enemyStats;
    private Slider slider;

    private void Start()
    {
        enemyStats = GetComponentInParent<EnemyStats>();

        slider = GetComponentInChildren<Slider>();

        enemyStats.onHealthChangeCallback += RefreshUI;

        slider.maxValue = enemyStats.GetMaxHealth();
        slider.value = slider.maxValue;
    }

    public void RefreshUI(int hp)
    {
        slider.value = hp;
    }
}
