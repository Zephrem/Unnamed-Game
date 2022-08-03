using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private BattleController battleController;

    private TextMeshProUGUI textGo;

    private void Awake()
    {
        battleController.onDamageChangeCallback += RefreshUI;
    }

    private void Start()
    {
        textGo = GetComponent<TextMeshProUGUI>();
    }

    public void RefreshUI(int damage)
    {
        textGo.text = damage.ToString();
    }
}
