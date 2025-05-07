using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using productions;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    private int currentIndex = 0;

    void Start()
    {
        //load all cards from the resources
        Card[] cards = Resources.LoadAll<Card>("CardData");

        allCards.AddRange(cards);

        HandManager hand = FindObjectOfType<HandManager>();
        for (int i  = 0; i < 6; i++) 
        {
            DrawCard(hand);
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
            return;

        Card nextCard = allCards[currentIndex];
        handManager.AddCardToHand(nextCard);
        currentIndex= (currentIndex+1)%allCards.Count;
    }
}
