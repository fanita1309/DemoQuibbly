using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using productions;
using System;

public class HandManager : MonoBehaviour
{
    public DeckManager deckManager;
    public GameObject cardPrefab; //assign cardprefab in the inspector
    public Transform handTransform; //root hand position;
    public float fanSpread =-7.5f;
    public float cardSpacing = 150f;
    public float verticalSpacing = 100f;
    public int maxHandSize = 12;
    public List<GameObject> cardsInHand = new List<GameObject>();


    void Start()
    {
        
    }

    public void AddCardToHand(Card cardData)
    {
        if (cardsInHand.Count < maxHandSize)
        {
            //instantiate the card
            GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
            cardsInHand.Add(newCard);

            newCard.GetComponent<CardDisplay>().cardData = cardData;
        }

        UpdateHandVisuals();
    }

    void Update()
    {
        UpdateHandVisuals();
    }

    private void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 1) 
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f );
            return;
        }

        for (int i = 0; i < cardCount; i++ ) 
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

           float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));

            float normalizedPosition = (2f * i / (cardCount - 1) - 1f);
            float verticalOffset =verticalSpacing * (1- normalizedPosition * normalizedPosition);

            //set card position
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f); 
        }

    }
}
