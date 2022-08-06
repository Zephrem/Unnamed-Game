using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageProgressUI : MonoBehaviour
{
    private Slider slider;
    private BattleController battleController;
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        battleController = FindObjectOfType<BattleController>();

        battleController.onProgressChangeCallback += RefreshUI;
        slider.maxValue = battleController.GetMaxStageProgress();
        slider.value = slider.minValue;
    }

    private void RefreshUI(int progress)
    {
        slider.value = progress;
    }
}
