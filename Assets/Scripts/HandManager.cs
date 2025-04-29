using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using productions;
using System;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab; //assign cardprefab in the inspector
    public Transform handTransform; //root hand position;
    public float fanSpread = 5f;
    public List<GameObject> cardsInHand = new List<GameObject>();


    void Start()
    {
        AddCardToHand();
    }

    public void AddCardToHand() 
    {
        //instantiate the card
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);
    }

}
