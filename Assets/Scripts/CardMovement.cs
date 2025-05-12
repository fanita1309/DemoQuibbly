using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private readonly int maxColumn=2;

    [SerializeField] private float selectScale = 1.2f;
    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector3 playPosition;
    [SerializeField] private GameObject glowEffect;
    [SerializeField] private GameObject playArrow;
    [SerializeField] private float lerpFactor = 0.1f;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = rectTransform.localScale;
        originalPosition= rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;
        gridManager = FindObjectOfType<GridManager>();
    }

    void Update()
    { 
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
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.GetComponent<GridCell>())
            {
                GridCell cell = hit.collider.GetComponent<GridCell>();
                Vector2 targetPos = cell.gridIndex;

                if (cell.gridIndex.x < maxColumn && gridManager.AddObjectToGrid(GetComponent<CardDisplay>().cardData.prefab, targetPos))
                {
                    HandManager handManager = FindAnyObjectByType<HandManager>();
                    DiscardManager discardManager = FindAnyObjectByType<DiscardManager>();
                    discardManager.AddToDiscard(GetComponent<CardDisplay>().cardData);
                    handManager.cardsInHand.Remove(gameObject);
                    handManager.UpdateHandVisuals();
                    Debug.Log("Placed Character");
                    Destroy(gameObject);
                }
            }
            TransitionToState0();
        }


        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }
}
    
