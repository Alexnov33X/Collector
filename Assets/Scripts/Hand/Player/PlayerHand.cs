using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

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
    /// Управляющая станция доски
    /// </summary>
    public GameBoardRegulator boardRegulator;

    /// <summary>
    /// Точка призыва
    /// </summary>
    public Transform SummonPoint;

    /// <summary>
    /// Точка взятия карты
    /// </summary>
    public Transform DeckLocation;

    /// <summary>
    /// Список, представляющий собой руку, хранит карты(их компонент CardEntity)
    /// </summary>
    public List<CardEntity> handList;

    /// <summary>
    /// Лист который будет записывать в себя карты, которые нужно будет убрать из руки
    /// </summary>
    private List<CardEntity> removeCardsList;
    private float delay = 0.25f;
    private float cardDrawDelay = 0.5f;

    public bool isPlayer; //if true - значит это рука игрока, иначе это рука оппонента

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Start()
    {
        handList = new List<CardEntity>(HandCapacity);
        removeCardsList = new List<CardEntity>();
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
    /// 
    public void ExecuteHandPhases()
    {
        StartCoroutine(Phases());
    }
    public IEnumerator Phases()
    {
        yield return StartCoroutine(CostReductionPhase());
        yield return new WaitForSeconds(0.75f);
        CardTransferPhase();
        yield return new WaitForSeconds(1);
        SummonPhase();
        yield return new WaitForSeconds(1);
        boardRegulator.OrderAttackToCells(isPlayer);
    }

    /// <summary>
    /// - Фаза снижения стоимости
    /// </summary>
    private IEnumerator CostReductionPhase()
    {
        foreach (CardEntity card in handList)
        {
            card.ReduceCardCost();
            yield return new WaitForSecondsRealtime(delay);
        }

        //EventBus.OnCardsInfoChanged?.Invoke();
    }

    /// <summary>
    /// - Фаза выдачи карты
    /// </summary>
    private void CardTransferPhase()
    {
        //Если в Боевой Деке не осталось карт, то пропускаем эту фазу
        if (PlayerBattleDeck.BattleDeck.Count <= 0)
        {
            return;
        }

        //Если в Руке не осталось места, то пропускаем фазу
        if (handList.Count() >= HandCapacity)
        {
            return;
        }

        CardScriptableObject transferedCard = PullRandomCard();

        GameObject newCardExample = Instantiate(CardPrefab, gameObject.transform);
        
        CardEntity newCardEntity = newCardExample.GetComponent<CardEntity>();
        //newCardExample.gameObject.SetActive(false);
        newCardEntity.InitializeCard(transferedCard);

        handList.Add(newCardEntity);
        //Transform tempLocation = newCardExample.transform;
        //newCardExample.transform.position = DeckLocation.position;
        //newCardExample.gameObject.SetActive(true);
        //StartCoroutine(MoveWithDelay(newCardExample, tempLocation, cardDrawDelay));
        EventBus.OnPlayerBatttleDeckAmountChanged?.Invoke();
    }
    private IEnumerator MoveWithDelay(GameObject go,Transform position, float time)
    {
        LeanTween.move(go, position, time).setEaseInOutSine();
        yield return new WaitForSecondsRealtime(time);
    }

    /// <summary>
    /// - Фаза призыва
    /// </summary>
    private void SummonPhase()
    {
        foreach (CardEntity card in handList)
        {
            if (card.cardData.CardCost <= 0)
            {
                //проверяет получилось ли призвать карту на доску
                if (boardRegulator.TrySummonCardToPlayerBoard(card, isPlayer))
                    removeCardsList.Add(card);
            }
        }

        //Удаляем из руки карты,которые ушли на доску
        foreach (CardEntity card in removeCardsList)
        {
            handList.Remove(card);
        }
        removeCardsList.Clear();
    }

    #region Additional Methods for Phases

    /// <summary>
    /// Вытаскивает рандомную карту из боевой колоды и удаляет ее от туда.
    /// </summary>
    /// <returns>Возвращает ScriptableObject карты</returns>     
    private CardScriptableObject PullRandomCard()
    {
        if (isPlayer)
        {
            int elementIndex = Random.Range(0, PlayerBattleDeck.BattleDeck.Count);

            CardScriptableObject card = PlayerBattleDeck.BattleDeck[elementIndex];
            PlayerBattleDeck.BattleDeck.RemoveAt(elementIndex);

            return card;
        }
        else
        {
            int elementIndex = Random.Range(0, PlayerBattleDeck.EnemyBattleDeck.Count);

            CardScriptableObject card = PlayerBattleDeck.EnemyBattleDeck[elementIndex];
            PlayerBattleDeck.EnemyBattleDeck.RemoveAt(elementIndex);

            return card;
        }
    }

    private void RemoveCardFromHand()
    {

    }
    #endregion
}
