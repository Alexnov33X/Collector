using System.Collections.Generic;

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
        PlayerDeck.CurrentDeck = ServerSurrogate.Instance.deckOnServer.currentDeck;
    }

    private static void SyncPlayerDecks()
    {
        ServerSurrogate.Instance.deckOnServer.currentDeck = PlayerDeck.CurrentDeck;
    }
}