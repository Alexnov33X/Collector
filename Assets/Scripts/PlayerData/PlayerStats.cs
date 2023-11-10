﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Хранит всю информацию, которую будем загружать с сервера: валюты, тиккеты и деки игрока.
/// </summary>
public static class PlayerStats
{
    /// <summary>
    /// Получаем инфо с сервера
    /// </summary>
    public static void LoadPlayerData()
    {
        LoadPlayerDecks();
    }

    /// <summary>
    /// Отправляем инфу на сервер(синк инфы с устройства на сервер)
    /// </summary>
    public static void SyncPlayerData()
    {
        SyncPlayerDecks();
    }

    private static void LoadPlayerDecks()
    {
        //временная затычка, пока мы без БД
        PlayerDecks.CurrentDeck = ServerSurrogate.Instance.currentDeckOnServer.currentDeck.OfType<CardScriptableObject>().ToList();
        PlayerDecks.CurrentEnemyDeck = ServerSurrogate.Instance.currentDeckOnServer.enemyCurrentDeck.OfType<CardScriptableObject>().ToList();

        //Инициализируем боевую колоду игрока
        PlayerBattleDeck.BattleDeck = PlayerDecks.CurrentDeck;
        PlayerBattleDeck.EnemyBattleDeck = PlayerDecks.CurrentEnemyDeck;
    }

    private static void SyncPlayerDecks()
    {
        ServerSurrogate.Instance.currentDeckOnServer.currentDeck = PlayerDecks.CurrentDeck.ToArray();
        ServerSurrogate.Instance.currentDeckOnServer.enemyCurrentDeck = PlayerDecks.CurrentEnemyDeck.ToArray();
    }
}