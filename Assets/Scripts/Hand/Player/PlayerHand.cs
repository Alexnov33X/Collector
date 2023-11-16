using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

/// <summary>
/// ��������. ������ � ���� ������ � ������ � ���� ������. ����� ������������ ��� ����.
/// ��������� ����� �� ���� � ���� � �� ���� �� �����
/// </summary>
public class PlayerHand : MonoBehaviour
{
    #region Constants

    /// <summary>
    /// ������������ ����������� ����
    /// </summary>
    private const int HandCapacity = 6;

    #endregion

    /// <summary>
    /// ������ �����
    /// </summary>
    public GameObject CardPrefab;

    /// <summary>
    /// ����������� ������� �����
    /// </summary>
    public GameBoardRegulator boardRegulator;

    /// <summary>
    /// ����� �������
    /// </summary>
    public Transform SummonPoint;

    /// <summary>
    /// ����� ������ �����
    /// </summary>
    public Transform DeckLocation;

    /// <summary>
    /// ������, �������������� ����� ����, ������ �����(�� ��������� CardEntity)
    /// </summary>
    public List<CardEntity> handList;

    /// <summary>
    /// ���� ������� ����� ���������� � ���� �����, ������� ����� ����� ������ �� ����
    /// </summary>
    private List<CardEntity> removeCardsList;
    private float cardReceiveDelay = 1;

    public bool isPlayer; //if true - ������ ��� ���� ������, ����� ��� ���� ���������
    private float delayBeforeSummon = 1;
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
        //���� ��������s �� ���������
        cardReceiveDelay = AnimationAndDelays.instance.cardReceiveDelay;
        delayBeforeSummon = AnimationAndDelays.instance.delayBeforeSummon;
        drawingCardAnimation = AnimationAndDelays.instance.drawingCardAnimation;
        DrawInnateCards();
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
    /// ��������� ���� ���� �� ������� ������������ ����:
    /// - ���� �������� ���������
    /// - ���� ������ �����
    /// - ���� �������
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
        yield return StartCoroutine(DrawCardPhase(1));
        yield return new WaitForSeconds(delayBeforeSummon);
        yield return SummonPhase(); //problem here
        //yield return new WaitForSeconds(summonCardAnimation);
        yield return boardRegulator.OrderAttackToCells(isPlayer);
        yield return boardRegulator.TurnEnd(isPlayer);
    }

    /// <summary>
    /// - ���� �������� ���������
    /// </summary>
    private IEnumerator CostReductionPhase()
    {
        foreach (CardEntity card in handList)
        {
            card.ReduceCardCost();
            yield return new WaitForSecondsRealtime(AnimationAndDelays.instance.cardCostChangeAnimation);
        }
        //����� ������ �������� � CardEntity, ��� �� ���������� �� ��������� ������ ����������� �������� ������� ����� ����
        //EventBus.OnCardsInfoChanged?.Invoke();
    }
    public GameObject cardFiddle;
    /// <summary>
    /// - ���� ������ �����
    /// </summary>
    public IEnumerator DrawCardPhase(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            //���� � ������ ���� �� �������� ����, �� ���������� ��� ����
            if (PlayerBattleDeck.BattleDeck.Count <= 0)
            {
                yield return new WaitForEndOfFrame();
            }

            //���� � ���� �� �������� �����, �� ���������� ����
            if (handList.Count() >= HandCapacity)
            {
                yield return new WaitForEndOfFrame();
            }

            CardScriptableObject transferedCard = PullRandomCard();

            GameObject newCardExample = Instantiate(CardPrefab, DeckLocation.transform);
            Debug.Log(transferedCard.Name);
            CardData.selectController(newCardExample, transferedCard.Name); //In cardData we create controller and get his type                                                                        
            CardEntity newCardEntity = newCardExample.GetComponent<CardEntity>();//Install controller into a card
                                                                                 //newCardEntity = newController;
            var fiddle = Instantiate(cardFiddle, gameObject.transform);
            yield return new WaitForEndOfFrame();
            newCardEntity.InitializeCard(transferedCard, !isPlayer);

            handList.Add(newCardEntity);

            yield return StartCoroutine(MoveWithDelay(newCardExample, fiddle.transform.position, AnimationAndDelays.instance.summonCardAnimation, fiddle));
        }
    }

    /// <summary>
    /// �������� ������� �������� ���� �� ������ � ����
    /// �� ���������
    /// �� ����� ��������, ����� ����� ����������, �� �����������
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
    /// - ���� �������
    /// </summary>
    private IEnumerator SummonPhase()
    {
        foreach (CardEntity card in handList)
        {
            if (card.cardData.CardCost <= 0)
            {
                //��������� ���������� �� �������� ����� �� �����
                if (boardRegulator.TrySummonCardToPlayerBoard(card, isPlayer))
                {
                    card.OnCardPlayed();
                    removeCardsList.Add(card);

                }
            }
        }

        //������� �� ���� �����,������� ���� �� �����
        foreach (CardEntity card in removeCardsList)
        {
            if (card.cardData.abilityAndStatus.ContainsKey(Enums.CardAbility.DrawCards))
                yield return new WaitForSeconds(AnimationAndDelays.instance.drawingCardAnimation * card.cardData.abilityAndStatus[Enums.CardAbility.DrawCards]); //waiting to draw cards
            handList.Remove(card);
            yield return new WaitForSeconds(AnimationAndDelays.instance.summonCardAnimation);
        }
        removeCardsList.Clear();
    }

    #region Additional Methods for Phases

    /// <summary>
    /// ����������� ��������� ����� �� ������ ������ � ������� �� �� ����.
    /// </summary>
    /// <returns>���������� ScriptableObject �����</returns>     
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
    public IEnumerator AddDefiniteCardToHand(CardScriptableObject transferedCard, bool instantSummon)
    {
        GameObject newCardExample = Instantiate(CardPrefab, DeckLocation.transform);
        CardData.selectController(newCardExample, transferedCard.Name); //In cardData we create controller and get his type                                                                        
        CardEntity newCardEntity = newCardExample.GetComponent<CardEntity>();//Install controller into a card
                                                                             //newCardEntity = newController;
        var fiddle = Instantiate(cardFiddle, gameObject.transform);
        yield return new WaitForEndOfFrame();
        newCardEntity.InitializeCard(transferedCard, !isPlayer);

        handList.Add(newCardEntity);

        yield return StartCoroutine(MoveWithDelay(newCardExample, fiddle.transform.position, AnimationAndDelays.instance.summonCardAnimation, fiddle));
        if (instantSummon)
        {
            newCardEntity.cardData.CardCost = 0; //reduce cost to 0 to summon later
            yield return SummonPhase();
        }
    }

    public void DrawAndSummonPartners()
    {
        var partners = PlayerBattleDeck.BattleDeck.FindAll(x => x.abilities.Contains(Enums.CardAbility.PartnerSummon));
        foreach (CardScriptableObject cardSO in partners)
        {
            CreatureSpawner.instance.spawnPartnerFromDeck(cardSO.Name, isPlayer, DeckLocation);
            PlayerBattleDeck.BattleDeck.Remove(cardSO);
        }
    }

    private void DrawInnateCards()
    {
        if (isPlayer)
        {
            var innateCards = PlayerBattleDeck.BattleDeck.FindAll(x => x.abilities.Contains(Enums.CardAbility.InnateCard));
            foreach (var card in innateCards)
            {
                GameObject newCardExample = Instantiate(CardPrefab, transform);
                CardData.selectController(newCardExample, card.Name); //In cardData we create controller and get his type                                                                        
                CardEntity newCardEntity = newCardExample.GetComponent<CardEntity>();//Install controller into a card
                newCardEntity.InitializeCard(card, !isPlayer);
                handList.Add(newCardEntity);
                PlayerBattleDeck.BattleDeck.Remove(card);
            }
        }
        else
        {
            var innateCards = PlayerBattleDeck.EnemyBattleDeck.FindAll(x => x.abilities.Contains(Enums.CardAbility.InnateCard));
            foreach (var card in innateCards)
            {
                GameObject newCardExample = Instantiate(CardPrefab, transform);
                CardData.selectController(newCardExample, card.Name); //In cardData we create controller and get his type                                                                        
                CardEntity newCardEntity = newCardExample.GetComponent<CardEntity>();//Install controller into a card
                newCardEntity.InitializeCard(card, !isPlayer);
                handList.Add(newCardEntity);
                PlayerBattleDeck.EnemyBattleDeck.Remove(card);
            }
        }
    }
    private void RemoveCardFromHand()
    {

    }
    #endregion
}
