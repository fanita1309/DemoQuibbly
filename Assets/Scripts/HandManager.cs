using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using productions;
using System;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab; //assign cardprefab in the inspector
    public Transform handTransform; //root hand position;
    public float fanSpread = 7.5f;
    public float cardSpacing =-100f;
    public float verticalSpacing = 10f;
    public List<GameObject> cardsInHand = new List<GameObject>();


    void Start()
    {
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
    }

    public void AddCardToHand() 
    {
        //instantiate the card
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        UpdateHandVisuals();
    }

    void Update()
    {
        UpdateHandVisuals();
    }

    private void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;
        for(int i = 0; i < cardCount; i++ ) 
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

           float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));
            float verticalOffset = (verticalSpacing * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f); 
        }

    }
}
