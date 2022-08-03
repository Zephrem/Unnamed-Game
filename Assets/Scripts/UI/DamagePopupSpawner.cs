using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject popupPre;

    private UnitStats stats;

    private void Start()
    {
        stats = GetComponentInParent<UnitStats>();
        stats.onDamagedCallback += SpawnPopup;
    }

    private void SpawnPopup(int damage)
    {
        GameObject newPopup = Instantiate(popupPre, gameObject.GetComponentInParent<GridController>().transform);
        newPopup.GetComponentInChildren<DamagePopup>().PopupSetup(transform.position, damage);
    }
}
