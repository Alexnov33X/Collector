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
    private float delay = 0.25f; //�������� � �������� ��� ��������
    private float cardReceiveDelay = 1;

    public bool isPlayer; //if true - ������ ��� ���� ������, ����� ��� ���� ���������
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
        delay = AnimationAndDelays.instance.cardCostChangeAnimation; //���� �������� �� ���������
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
        yield return StartCoroutine(CostReductionPhase());
        yield return new WaitForSeconds(cardReceiveDelay);
        CardTransferPhase();
        yield return new WaitForSeconds(delayBeforeSummon);
        yield return SummonPhase();
        //yield return new WaitForSeconds(summonCardAnimation);
        boardRegulator.OrderAttackToCells(isPlayer);
    }

    /// <summary>
    /// - ���� �������� ���������
    /// </summary>
    private IEnumerator CostReductionPhase()
    {
        foreach (CardEntity card in handList)
        {
            card.ReduceCardCost();
            yield return new WaitForSecondsRealtime(delay);
        }
        //����� ������ �������� � CardEntity, ��� �� ���������� �� ��������� ������ ����������� �������� ������� ����� ����
        //EventBus.OnCardsInfoChanged?.Invoke();
    }

    /// <summary>
    /// - ���� ������ �����
    /// </summary>
    private void CardTransferPhase()
    {
        //���� � ������ ���� �� �������� ����, �� ���������� ��� ����
        if (PlayerBattleDeck.BattleDeck.Count <= 0)
        {
            return;
        }

        //���� � ���� �� �������� �����, �� ���������� ����
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

        CardEntity newCardEntity = newCardExample.GetComponent<CardEntity>(); //��� ������� ���� ������� ����������� ������ ����� �� ������
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
    /// �������� ������� �������� ���� �� ������ � ����
    /// �� ���������
    /// �� ����� ��������, ����� ����� ����������, �� �����������
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
                    removeCardsList.Add(card);
            }
        }

        //������� �� ���� �����,������� ���� �� �����
        foreach (CardEntity card in removeCardsList)
        {
            handList.Remove(card);
            yield return new WaitForSeconds(summonCardAnimation);
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

    private void RemoveCardFromHand()
    {

    }
    #endregion
}
