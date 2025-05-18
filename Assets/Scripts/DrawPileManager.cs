using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using productions;
using TMPro;
using System;

public class DrawPileManager : MonoBehaviour
{
    public DeckOwner owner;
    public List<Card> drawPile = new List<Card>();

    public int startingHandSize = 6;
    private int currentIndex = 0;
    public int maxHandSize;
    public int currentHandSize;
    private HandManager handManager;
    private DiscardManager discardManager;
    public TextMeshProUGUI drawPileCounter;

    void Start()
    {
        if (owner == DeckOwner.Player)
        {
            handManager = FindObjectOfType<HandManager>();
        }
    }

    void Update()
    {
        if (owner == DeckOwner.Player && handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void MakeDrawPile(List<Card> cardsToAdd)
    {
        drawPile.Clear(); // Por si acaso
        drawPile.AddRange(cardsToAdd);
        Utility.Shuffle(drawPile);
        UpdateDrawPileCount();
    }

    public void BattleSetup(int numberOfCardsToDraw, int setMaxHandSize, HandManager handManager, CardOwner owner)
    {
        maxHandSize = setMaxHandSize;
        for (int i = 0; i < numberOfCardsToDraw; i++)
        {
            DrawCard(handManager, owner);
        }
    }

    public void DrawCard(HandManager handManager, CardOwner owner)
    {
        currentHandSize = handManager.cardsInHand.Count;

        if (drawPile.Count == 0)
        {
            RefillDeckFromDiscard(owner);
        }

        if (drawPile.Count == 0) return; // Si sigue vacío, salir

        if (currentHandSize < maxHandSize)
        {
            if (currentIndex >= drawPile.Count)
            {
                currentIndex = 0;
            }

            Card nextCard = drawPile[currentIndex];
            handManager.AddCardToHand(nextCard);
            drawPile.RemoveAt(currentIndex);
            UpdateDrawPileCount();

            if (drawPile.Count > 0)
            {
                currentIndex = currentIndex % drawPile.Count;
            }
            else
            {
                currentIndex = 0;
            }
        }
    }

    public void RefillDeckFromDiscard(CardOwner owner)
    {
        if (discardManager == null)
        {
            discardManager = FindObjectOfType<DiscardManager>();
        }

        if (discardManager != null)
        {
            int discardCount = (owner == CardOwner.Player) ? discardManager.playerDiscardCount : discardManager.enemyDiscardCount;

            if (discardCount > 0)
            {
                drawPile = discardManager.PullAllFromDiscard(owner);
                Utility.Shuffle(drawPile);
                currentIndex = 0;
            }
        }
    }

    private void UpdateDrawPileCount()
    {
        if (drawPileCounter != null)
            drawPileCounter.text = drawPile.Count.ToString();
    }
}
