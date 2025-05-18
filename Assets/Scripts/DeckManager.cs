using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using productions;
using System;

public enum DeckOwner { Player, Enemy }

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    public List<Card> playerCards = new List<Card>();
    public List<Card> enemyCards = new List<Card>();

    public int startingHandSize = 6;
    public int maxHandSize = 12;

    private HandManager playerHandManager;
    private DrawPileManager playerDrawPileManager;

    private HandManager enemyHandManager;
    private DrawPileManager enemyDrawPileManager;

    private bool startBattleRun = true;

    void Awake()
    {
        // Encuentra los HandManagers por tag o por nombre si tienes más de uno
        HandManager[] allHandManagers = FindObjectsOfType<HandManager>();
        DrawPileManager[] allDrawManagers = FindObjectsOfType<DrawPileManager>();

        foreach (var hm in allHandManagers)
        {
            if (hm.owner == DeckOwner.Player) playerHandManager = hm;
            else if (hm.owner == DeckOwner.Enemy) enemyHandManager = hm;
        }

        foreach (var dm in allDrawManagers)
        {
            if (dm.owner == DeckOwner.Player) playerDrawPileManager = dm;
            else if (dm.owner == DeckOwner.Enemy) enemyDrawPileManager = dm;
        }
    }

    void Start()
    {
        // Carga todas las cartas del folder Resources/CardData
        Card[] cards = Resources.LoadAll<Card>("CardData");

        foreach (Card card in cards)
        {
            allCards.Add(card);

            if (card.owner == CardOwner.Player)
            {
                playerCards.Add(card);
            }
            else if (card.owner == CardOwner.Enemy)
            {
                enemyCards.Add(card);
            }
        }
    }

    void Update()
    {
        if (startBattleRun)
        {
            BattleSetup();
        }
    }

    public void BattleSetup()
    {
        playerHandManager.BattleSetup(maxHandSize);
        playerDrawPileManager.MakeDrawPile(playerCards);
        playerDrawPileManager.BattleSetup(startingHandSize, maxHandSize, playerHandManager, CardOwner.Player);

        if (enemyDrawPileManager != null && enemyHandManager != null)
        {
            enemyHandManager.BattleSetup(maxHandSize);
            enemyDrawPileManager.MakeDrawPile(enemyCards);
            enemyDrawPileManager.BattleSetup(startingHandSize, maxHandSize, enemyHandManager, CardOwner.Enemy);
        }

        startBattleRun = false;
    }
}
