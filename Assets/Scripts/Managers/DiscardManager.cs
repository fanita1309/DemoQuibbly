using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using productions;
using TMPro;
using static productions.Card;
using System;

public class DiscardManager : MonoBehaviour
{
    [SerializeField] public List<Card> playerDiscardCards = new List<Card>();
    [SerializeField] public List<Card> enemyDiscardCards = new List<Card>();

    public TextMeshProUGUI discardCountPlayer;
    public TextMeshProUGUI discardCountEnemy;

    public int playerDiscardCount;
    public int enemyDiscardCount;

    void Awake()
    {
        UpdateDiscardCounts();
    }

    private void UpdateDiscardCounts()
    {
        if (discardCountPlayer != null)
            discardCountPlayer.text = playerDiscardCards.Count.ToString();
        if (discardCountEnemy != null)
            discardCountEnemy.text = enemyDiscardCards.Count.ToString();

        playerDiscardCount = playerDiscardCards.Count;
        enemyDiscardCount = enemyDiscardCards.Count;
    }

    public void AddToDiscard(Card card)
    {
        if (card != null)
        {
            if (card.owner == CardOwner.Player)
            {
                playerDiscardCards.Add(card);
            }
            else
            {
                enemyDiscardCards.Add(card);
            }
            UpdateDiscardCounts();
        }
    }

    public Card PullFromDiscard()
    {
        return PullFromDiscard(CardOwner.Player);
    }

    public Card PullFromDiscard(CardOwner owner)
    {
        List<Card> pile = owner == CardOwner.Player ? playerDiscardCards : enemyDiscardCards;

        if (pile.Count > 0)
        {
            Card cardToReturn = pile[pile.Count - 1];
            pile.RemoveAt(pile.Count - 1);
            UpdateDiscardCounts();
            return cardToReturn;
        }
        else
        {
            return null;
        }
    }

    public bool PullSelectCardFromDiscard(Card card)
    {
        if (card == null) return false;

        List<Card> pile = card.owner == CardOwner.Player ? playerDiscardCards : enemyDiscardCards;

        if (pile.Count > 0 && pile.Contains(card))
        {
            pile.Remove(card);
            UpdateDiscardCounts();
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<Card> PullAllFromDiscard(CardOwner owner)
    {
        List<Card> pile = owner == CardOwner.Player ? playerDiscardCards : enemyDiscardCards;

        if (pile.Count > 0)
        {
            List<Card> cardsToReturn = new List<Card>(pile);
            pile.Clear();
            UpdateDiscardCounts();
            return cardsToReturn;
        }
        else
        {
            return new List<Card>();
        }
    }
}
