using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Сущность. Хранит в себе данные о картах в деках игроков. Может обрабатывать эту инфу.
/// Возможно стоит разделить на PlayerDeck и EnemyDeck.
/// </summary>
public static class PlayerDeck
{
    public static List<CardInfo> playerDeck; 
    public static List<CardInfo> enemyDeck;

    public static PlayerHandController playerHand;
    public static PlayerHandController enemyHand;

    private static void DrawCard(bool forPlayer)
    {
        if (forPlayer && playerDeck.Count > 0)
        {
            CardInfo randomCard = playerDeck[Random.Range(0, playerDeck.Count)];
            playerHand.AddCardToHand(randomCard);
            playerDeck.Remove(randomCard);
        }
        else if (!forPlayer && enemyDeck.Count > 0)
        {
            CardInfo randomCard = enemyDeck[Random.Range(0, enemyDeck.Count)];
            enemyHand.AddCardToHand(randomCard);
            enemyDeck.Remove(randomCard);
        }
    }
}
