using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Дествующая колода игрока, которая используется для боевки. Инициализируется при начале боя(при загрузке сцены BattleScene) и 
/// используется только для боя. Инициализация происходит в BattleBootstrapp.
/// </summary>
public static class PlayerBattleDeck
{
    private static List<CardScriptableObject> battleDeck = new List<CardScriptableObject>();
    public static List<CardScriptableObject> BattleDeck { get { return battleDeck; } set => battleDeck = value; }

    /// <summary>
    /// Добавление карты в колоду
    /// Пока что такой механики нет. Задел на будущее.
    /// </summary>
    public static void AddCard()
    {

    }

    /// <summary>
    /// Удаление карты из колоды
    /// </summary>
    public static CardScriptableObject TransferCardToHand()
    {
        int elementIndex = Random.Range(0, battleDeck.Count);

        CardScriptableObject card = battleDeck[elementIndex];
        battleDeck.RemoveAt(elementIndex);

        return card;
    }
}
