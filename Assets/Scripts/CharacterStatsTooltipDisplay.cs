using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CharacterStatsTooltip : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI cardTypeText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI damageTypeText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI attackPatternText;
    public TextMeshProUGUI priorityTargetText;

    private RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    [SerializeField] private float lerpFactor = 0.1f;
    [SerializeField] private float xOffset = 200f;
    private Canvas canvas;



    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // Buscar el Canvas más cercano en este objeto o en sus padres
        canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }

        if (canvas == null)
        {
            Debug.LogError("Canvas not found in CharacterStatsTooltip or its parents.");
        }

        // Obtener el CanvasGroup del mismo objeto donde está este script
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found on CharacterStatsTooltip.");
        }
    }

    

    public void SetStatsText(CharacterStats stats)
    {
        nameText.text = $"{stats.cardName} stats";
        cardTypeText.text = string.Join(", ", stats.cardType);
        healthText.text = stats.health.ToString();
        damageText.text = $"{stats.damageMin} - {stats.damageMax}";
        damageTypeText.text = string.Join(", ", stats.damageType);
        rangeText.text = stats.range.ToString();
        attackPatternText.text = stats.attackPattern.ToString();
        priorityTargetText.text = stats.priorityTarget.ToString();
    }

    public bool IsMouseOverTooltip()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 localMousePosition = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            Input.mousePosition,
            null,
            out localMousePosition
        );
        return rectTransform.rect.Contains(localMousePosition);
    }
}
