using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using productions;

public class CharacterTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CharacterStatsTooltip tooltip;
    public float fadeTime = 0.1f;
    public float hideDelay = 0.15f;

    void Awake()
    {
        tooltip = FindObjectOfType<CharacterStatsTooltip>();
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        if (tooltip != null)
        {
            tooltip.SetStatsText(GetComponent<CharacterStats>());
            StopAllCoroutines(); // 👈 Cancela cualquier intento previo de ocultar
            StartCoroutine(Utility.FadeIn(tooltip.canvasGroup, 1.0f, fadeTime));
        }
        else tooltip = FindObjectOfType<CharacterStatsTooltip>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltip != null)
        {
            StartCoroutine(Utility.FadeOut(tooltip.canvasGroup, 0.0f, fadeTime));
        }
    }

    private IEnumerator DelayedHide()
    {
        yield return new WaitForSeconds(hideDelay);

        if (!tooltip.IsMouseOverTooltip())
        {
            StartCoroutine(Utility.FadeOut(tooltip.canvasGroup, 0.0f, fadeTime));
        }
    }
}
