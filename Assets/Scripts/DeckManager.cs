using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using productions;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    public int startingHandSize = 6;
    private int currentIndex = 0;
    public int maxHandSize;
    public int currentHandSize;
    private HandManager handManager;

    void Start()
    {
        //load all cards from the resources
        Card[] cards = Resources.LoadAll<Card>("CardData");

        allCards.AddRange(cards);

        handManager= FindObjectOfType<HandManager>();
        maxHandSize = handManager.maxHandSize;
        for (int i  = 0; i < startingHandSize; i++) 
        {
            DrawCard(handManager);
        }
    }

    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
            return;

        if (currentHandSize < maxHandSize)
        {
            Card nextCard = allCards[currentIndex];
            handManager.AddCardToHand(nextCard);
            currentIndex = (currentIndex + 1) % allCards.Count;
        }

    }
}
