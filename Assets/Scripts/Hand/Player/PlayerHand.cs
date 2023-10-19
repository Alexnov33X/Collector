using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Сущность. Хранит в себе данные о картах в руке игрока. Может обрабатывать эту инфу.
/// Переносит карту из деки в руку и из руки на доску
/// </summary>
public class PlayerHand : MonoBehaviour
{
    #region Constants

    /// <summary>
    /// Максимальная вместимость руки
    /// </summary>
    private const int HandCapacity = 6;

    #endregion

    /// <summary>
    /// Префаб карты
    /// </summary>
    public GameObject CardPrefab;

    /// <summary>
    /// Список, представляющий собой руку, хранит карты(их компонент CardEntity)
    /// </summary>
    private List<CardEntity> handList;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        handList = new List<CardEntity>(HandCapacity);
    }

    #region Methods to get or set info to handList
    public int GetHandLength()
    {
        return handList.Count;
    }

    public CardData GetCardDataFromIndex(int i)
    {
        return handList[i].cardData;
    }
    #endregion

    /// <summary>
    /// Выполняет фазы хода за которые ответственна рука:
    /// - Фаза снижения стоимости
    /// - Фаза выдачи карты
    /// - Фаза призыва
    /// </summary>
    public void ExecuteHandPhases()
    {
        CostReductionPhase();
        CardTransferPhase();
        SummonPhase();
    }

    /// <summary>
    /// - Фаза снижения стоимости
    /// </summary>
    private void CostReductionPhase()
    {
        foreach (CardEntity card in handList)
        {
            card.ReduceCardCost();
        }

        EventBus.OnCardsInfoChanged?.Invoke();
    }

    /// <summary>
    /// - Фаза выдачи карты
    /// </summary>
    private void CardTransferPhase()
    {
        if (handList.Count() < 6)
        {
            CardScriptableObject transferedCard = PullRandomCard();

            GameObject newCardExample = Instantiate(CardPrefab, gameObject.transform);

            CardEntity newCardEntity = newCardExample.GetComponent<CardEntity>();
            newCardEntity.InitializeCard(transferedCard);

            handList.Add(newCardEntity);

            EventBus.OnPlayerDeckCardsChanged?.Invoke();
        }
    }

    /// <summary>
    /// - Фаза призыва
    /// </summary>
    private void SummonPhase()
    {

    }

    #region Additional Methods for Phases

    /// <summary>
    /// Вытаскивает рандомную карту из боевой колоды и удаляет ее от туда.
    /// </summary>
    /// <returns>Возвращает ScriptableObject карты</returns>     
    private CardScriptableObject PullRandomCard()
    {
        int elementIndex = Random.Range(0, PlayerBattleDeck.BattleDeck.Count);

        CardScriptableObject card = PlayerBattleDeck.BattleDeck[elementIndex];
        PlayerBattleDeck.BattleDeck.RemoveAt(elementIndex);

        return card;
    }
    
    private void RemoveCardFromHand()
    {

    }
    #endregion
}
