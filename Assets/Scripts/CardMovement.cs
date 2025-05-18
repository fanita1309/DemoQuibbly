using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using productions;
using UnityEditor.U2D.Animation;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;
    private Vector3 originalScale;
    private int currentState=0;
    private Quaternion originalRotation;
    private Vector3 originalPosition;
    private GridManager gridManager;
    private readonly int maxColumn=5;

    [SerializeField] private float selectScale = 1.2f;
    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector3 playPosition;
    [SerializeField] private GameObject glowEffect;
    [SerializeField] private GameObject playArrow;
    [SerializeField] private float lerpFactor = 0.1f;

    private LayerMask gridLayerMask;
    private LayerMask characterLayerMask;
    private Card cardData;
    private CardDisplay cardDisplay;
    HandManager handManager;
    DiscardManager discardManager;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;

       // gridManager = FindObjectOfType<GridManager>();
        handManager = FindObjectOfType<HandManager>();
        discardManager = FindObjectOfType<DiscardManager>();
        cardDisplay = GetComponent<CardDisplay>();

        gridLayerMask = LayerMask.GetMask("Grid");
        characterLayerMask = LayerMask.GetMask("Characters");
        cardData = cardDisplay.cardData;

    }

    void Update()
    { 
        if (cardData != cardDisplay.cardData)
        {
            cardData = cardDisplay.cardData;
        }

        switch (currentState) 
        {
            case 1:
                HandleHoverState();
                break;

            case 2:
                HandleDragState();
                if (!Input.GetMouseButton(0))
                {
                    TransitionToState0();
                }
                break;
            case 3:
                HandlePlayState();
            
                break;

        }

    }


    private void TransitionToState0()
    {
        currentState = 0;
        GameManager.Instance.playingCard= false;
        rectTransform.localScale = originalScale;
        rectTransform.localPosition = originalPosition;
        rectTransform.localRotation = originalRotation;
        glowEffect.SetActive(false); //disable glow
        playArrow.SetActive(false); //disable play arrow
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;

            currentState = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if ( currentState == 1)
        {
            TransitionToState0();
        }
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        if (currentState == 1)
        {
            currentState = 2;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
            originalPanelLocalPosition = rectTransform.localPosition;
        }
    }

    public void OnDrag (PointerEventData eventData)
    {
        if (currentState == 2)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPointerPosition)) 
            {
                //localPointerPosition /= canvas.scaleFactor;
                //Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
                // rectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;
                rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFactor);

                if (rectTransform.localPosition.y > cardPlay.y)
                {
                    currentState = 3;
                    playArrow.SetActive(true);
                    rectTransform.localPosition = Vector3.Lerp(rectTransform.position, playPosition, lerpFactor);
                }
            }
        }
    }

    private void HandleHoverState()
    {
        glowEffect.SetActive(true);    
        rectTransform.localScale = originalScale * selectScale;
    }

    private void HandleDragState()
    {
        //set the cards rotation to zero
        rectTransform.localRotation = Quaternion.identity;
    }

    private void HandlePlayState()
    {
        if (!GameManager.Instance.playingCard)
        {
            GameManager.Instance.playingCard = true;
        }

        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;

        if (!Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //specific card changes
            if (cardData is Character characterCard)
            {
                TryToPlayCharacterCard(ray, characterCard);
            }
            else if (cardData is Spell spellCard)
            {
                TryToPlaySpellCard(ray, spellCard);
            }

            TransitionToState0();
        }

        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }

    private void TryToPlayCharacterCard(Ray ray, Character characterCard)
    {
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, gridLayerMask);

        if (hit.collider != null && hit.collider.TryGetComponent<GridCell>(out var cell))
        {
            Vector2 targetPos = cell.gridIndex;
            GridManager gridManagerOfCell = cell.GetComponentInParent<GridManager>();

            if (gridManagerOfCell != null && cell.gridIndex.x < maxColumn && gridManagerOfCell.AddObjectToGrid(characterCard.prefab, targetPos))
            {
                cell.objectInCell.GetComponent<CharacterStats>().characterStartData = characterCard;
                discardManager.AddToDiscard(cardData);
                handManager.cardsInHand.Remove(gameObject);
                handManager.UpdateHandVisuals();
                Debug.Log($"Placed Character {characterCard.prefab}");
                Destroy(gameObject);
            }
        }
    }

    private void TryToPlaySpellCard(Ray ray, Spell spellCard)
    {
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, gridLayerMask);

        if (hit.collider != null && hit.collider.TryGetComponent<GridCell>(out GridCell cell))
        {
            GridManager gridManagerOfCell = cell.GetComponentInParent<GridManager>();
            GameObject objInCell = cell.objectInCell;

            if (objInCell != null && objInCell.TryGetComponent<CharacterStats>(out var targetStats))
            {
                SpellEffectApplier.ApplySpell(spellCard, targetStats);
                discardManager.AddToDiscard(cardData);
                handManager.cardsInHand.Remove(gameObject);
                handManager.UpdateHandVisuals();
                Debug.Log($"Placed spell {spellCard.name} on {objInCell.name}");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("No character found in cell to apply the spell.");
            }
        }
    }
}

    
