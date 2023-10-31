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
    private float delay = 0.25f; //задержка в секундах для анимаций
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
        delay = AnimationAndDelays.instance.cardCostChangeAnimation; //берём параметр из хранилища
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
        yield return StartCoroutine(CostReductionPhase());
        yield return new WaitForSeconds(cardReceiveDelay);
        CardTransferPhase();
        yield return new WaitForSeconds(delayBeforeSummon);
        yield return SummonPhase();
        //yield return new WaitForSeconds(summonCardAnimation);
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
        //Вызов ивента переехал в CardEntity, это не оптимально но добавляет эффект постепенных анимаций которые хотел Саня
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

        GameObject newCardExample = Instantiate(CardPrefab, DeckLocation.transform);
<<<<<<< Updated upstream
        GameObject fiddle = Instantiate(CardPrefab, gameObject.transform);
        fiddle.SetActive(true);
        //fiddle = Instantiate(fiddle, gameObject.transform);
        //TestCard.transform.position = DeckLocation.position;
=======
>>>>>>> Stashed changes

        CardEntity newCardEntity = newCardExample.GetComponent<CardEntity>(); //тут остатки кода попыток анимировать взятие карты из колоды
        var fiddle = new GameObject("A"); //IT FUCKING WORKS
        var rect = fiddle.AddComponent<RectTransform>();
        fiddle.transform.SetParent(transform);
        fiddle.transform.localScale = Vector3.one;
        newCardEntity.InitializeCard(transferedCard);
        StartCoroutine(MoveWithDelay(newCardExample, fiddle.transform.position, 0.5f, fiddle));
        handList.Add(newCardEntity);
        //Transform tempLocation = newCardExample.transform;
        //newCardExample.transform.position = DeckLocation.position;
        //newCardExample.gameObject.SetActive(true);
<<<<<<< Updated upstream
        //StartCoroutine(MoveWithDelay(newCardExample, tempLocation, cardDrawDelay));
=======
        StartCoroutine(MoveWithDelay(newCardExample, rect.transform, 0.5f, fiddle));
>>>>>>> Stashed changes
        //EventBus.OnPlayerBatttleDeckAmountChanged?.Invoke();
    }

    /// <summary>
    /// Пытались сделать движение карт из колоды в руку
    /// Не прокатило
    /// Но метод оставили, может потом пригодится, он универсален
    /// </summary>
<<<<<<< Updated upstream
    private IEnumerator MoveWithDelay(GameObject go, Vector3 position, float time, GameObject another)
=======
    private IEnumerator MoveWithDelay(GameObject go, Transform position, float time, GameObject fiddle)
>>>>>>> Stashed changes
    {
        LeanTween.move(go, position, time).setEaseInOutSine();
        //LeanTween.move()
        EventBus.OnPlayerBatttleDeckAmountChanged?.Invoke();
        yield return new WaitForSecondsRealtime(time);
<<<<<<< Updated upstream
        go.transform.SetParent(gameObject.transform);
        //Destroy(another);
        EventBus.OnPlayerBatttleDeckAmountChanged?.Invoke();
=======
        go.transform.SetParent(transform);
        Destroy(fiddle);
        
>>>>>>> Stashed changes
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
