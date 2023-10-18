using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Сущность. Хранит в себе данные о картах в деках игрока. Обрабатывает инфу о картах для корректного билдинга дек.
/// </summary>
public static class PlayerDeck
{
    /// <summary>
    /// Константы, по геймплею:
    /// Максимальное кол-во карт
    /// В колоде может быть не более 1 легендарной карты из 1 вселенной
    /// ... и т.д.
    /// </summary>
    #region Consts
    private const int maxCardNumber = 14;
    #endregion

    private static List<CardScriptableObject> currentDeck;
    public static List<CardScriptableObject> CurrentDeck { get { return currentDeck; } set => currentDeck = value; }

    /// <summary>
    /// Добавление карты в колоду
    /// </summary>
    public static void AddCard(CardScriptableObject card) 
    {
        ValidateCardAddition(card);
    }

    /// <summary>
    /// Удаление карты из колоды
    /// </summary>
    public static void RemoveCard()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    private static bool ValidateCardAddition(CardScriptableObject cardToValidate)
    {
        //будет проверять можно ли добавить эту карту в деку, согласно всем необходимым правилам Декбилдинга
        return true;
    }

    //Методы описывающие правила декбилдинга(В сути своей просто будут возвращать bool true, если карта соответсвует конкретному правилу и наоборот)
    
}
