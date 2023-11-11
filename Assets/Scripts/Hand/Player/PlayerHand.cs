using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

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
    private float cardReceiveDelay = 1;

    public bool isPlayer; //if true - значит это рука игрока, иначе это рука оппонента
    private float delayBeforeSummon = 1;
    private float summonCardAnimation = 0.5f;
    private float drawingCardAnimation = 1f;

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
        //берём параметрs из хранилища
        cardReceiveDelay = AnimationAndDelays.instance.cardReceiveDelay;
        delayBeforeSummon = AnimationAndDelays.instance.delayBeforeSummon;
        summonCardAnimation = AnimationAndDelays.instance.summonCardAnimation;
        drawingCardAnimation = AnimationAndDelays.instance.drawingCardAnimation;
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
    public IEnumerator ExecuteHandPhases()
    {
        yield return StartCoroutine(Phases());
    }
    public IEnumerator Phases()
    {
        yield return boardRegulator.TurnStart(isPlayer);
        yield return StartCoroutine(CostReductionPhase());
        yield return new WaitForSeconds(cardReceiveDelay);
        yield return StartCoroutine(CardTransferPhase());
        yield return new WaitForSeconds(delayBeforeSummon);
        yield return SummonPhase();
        //yield return new WaitForSeconds(summonCardAnimation);
        yield return boardRegulator.OrderAttackToCells(isPlayer);
        yield return boardRegulator.TurnEnd(isPlayer);
    }

    /// <summary>
    /// - Фаза снижения стоимости
    /// </summary>
    private IEnumerator CostReductionPhase()
    {
        foreach (CardEntity card in handList)
        {
            card.ReduceCardCost();
            yield return new WaitForSecondsRealtime(AnimationAndDelays.instance.cardCostChangeAnimation);
        }
        //Вызов ивента переехал в CardEntity, это не оптимально но добавляет эффект постепенных анимаций которые хотел Саня
        //EventBus.OnCardsInfoChanged?.Invoke();
    }
    public GameObject cardFiddle;
    /// <summary>
    /// - Фаза выдачи карты
    /// </summary>
    private IEnumerator CardTransferPhase()
    {
        //Если в Боевой Деке не осталось карт, то пропускаем эту фазу
        if (PlayerBattleDeck.BattleDeck.Count <= 0)
        {
            yield return new WaitForEndOfFrame();
        }

        //Если в Руке не осталось места, то пропускаем фазу
        if (handList.Count() >= HandCapacity)
        {
            yield return new WaitForEndOfFrame();
        }

        CardScriptableObject transferedCard = PullRandomCard();

        GameObject newCardExample = Instantiate(CardPrefab, DeckLocation.transform);
        Debug.Log(transferedCard.Name);
        CardData.selectController(newCardExample, transferedCard.Name); //In cardData we create controller and get his type
        //CardEntity currentController = newCardExample.GetComponent<CardEntity>();
        //newController.ha
        CardEntity newCardEntity = newCardExample.GetComponent<CardEntity>();//Install controller into a card
        //newCardEntity = newController;
        var fiddle = Instantiate(cardFiddle, gameObject.transform);
        yield return new WaitForEndOfFrame();
        newCardEntity.InitializeCard(transferedCard, !isPlayer);

        handList.Add(newCardEntity);

        yield return StartCoroutine(MoveWithDelay(newCardExample, fiddle.transform.position, 0.5f, fiddle));
    }

    /// <summary>
    /// Пытались сделать движение карт из колоды в руку
    /// Не прокатило
    /// Но метод оставили, может потом пригодится, он универсален
    /// </summary>
    private IEnumerator MoveWithDelay(GameObject go, Vector3 position, float time, GameObject fiddle)
    {
        var x = go.GetComponent<RectTransform>();
        LeanTween.move(go, position, time);
        EventBus.OnPlayerBatttleDeckAmountChanged?.Invoke();
        yield return new WaitForSecondsRealtime(time);
        go.transform.SetParent(transform, true);
        Destroy(fiddle);
    }

    /// <summary>
    /// - Фаза призыва
    /// </summary>
    private IEnumerator SummonPhase()
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
            yield return new WaitForSeconds(summonCardAnimation);
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
