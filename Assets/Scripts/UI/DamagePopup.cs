using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private Vector2 initialPos;
    private Vector2 finalPos;

    [SerializeField] private int finalX, finalY;
    [SerializeField] private Color initialColor, finalColor;
    [SerializeField] private float fadeDuration;

    private float fadeStartTime;
    private TextMeshProUGUI textComp;

    private void Awake()
    {
        textComp = GetComponentInChildren<TextMeshProUGUI>();
        fadeStartTime = Time.time;
    }

    private void Update()
    {
        float progress = (Time.time - fadeStartTime) / fadeDuration;

        if (progress <= 1)
        {
            transform.position = Vector2.Lerp(initialPos, finalPos, progress);
            textComp.color = Color.Lerp(initialColor, finalColor, progress);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PopupSetup(Vector2 pos, int damage)
    {
        initialPos = pos;

        finalPos = new Vector2(pos.x + finalX, pos.y + finalY);

        textComp.text = damage.ToString();
    }
}
