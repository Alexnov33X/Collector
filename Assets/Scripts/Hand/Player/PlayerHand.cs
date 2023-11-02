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
        yield return StartCoroutine(CardTransferPhase());
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
    public GameObject cardFiddle;
    /// <summary>
    /// - ���� ������ �����
    /// </summary>
    private IEnumerator CardTransferPhase()
    {
        //���� � ������ ���� �� �������� ����, �� ���������� ��� ����
        if (PlayerBattleDeck.BattleDeck.Count <= 0)
        {
            yield return new WaitForSecondsRealtime(0.01f); ;
        }

        //���� � ���� �� �������� �����, �� ���������� ����
        if (handList.Count() >= HandCapacity)
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }

        CardScriptableObject transferedCard = PullRandomCard();

        GameObject newCardExample = Instantiate(CardPrefab, DeckLocation.transform);

        CardEntity newCardEntity = newCardExample.GetComponent<CardEntity>(); //��� ������� ���� ������� ����������� ������ ����� �� ������
        var fiddle = Instantiate(cardFiddle, gameObject.transform);

       // yield return StartCoroutine(NewMethod(fiddle));
        Debug.Log("Courutine done");
        
        newCardEntity.InitializeCard(transferedCard, !isPlayer);
        
        handList.Add(newCardEntity);
        //Transform tempLocation = newCardExample.transform;
        //newCardExample.transform.position = DeckLocation.position;
        //newCardExample.gameObject.SetActive(true);
        yield return StartCoroutine(MoveWithDelay(newCardExample, fiddle.transform.position, 0.5f, fiddle));
        Debug.Log(newCardExample.transform.position.z);
        //EventBus.OnPlayerBatttleDeckAmountChanged?.Invoke();
    }

    //private IEnumerator NewMethod(GameObject fiddle)
    //{
    //    //fiddle.transform.SetParent(transform);
    //    //fiddle.transform.localScale = Vector3.one;
    //    fiddle.transform.position = new Vector3(fiddle.transform.position.x, fiddle.transform.position.y, 0);
    //    LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    //    fiddle.transform.position = new Vector3(fiddle.transform.position.x, fiddle.transform.position.y, 0);
    //    yield return new WaitForEndOfFrame();
    //    fiddle.transform.position = new Vector3(fiddle.transform.position.x, fiddle.transform.position.y, 0);
    //    // Debug.Log(rect.position);
    //    Debug.Log(fiddle.transform.position);
    //}
    /// <summary>
    /// �������� ������� �������� ���� �� ������ � ����
    /// �� ���������
    /// �� ����� ��������, ����� ����� ����������, �� �����������
    /// </summary>
    private IEnumerator MoveWithDelay(GameObject go, Vector3 position, float time, GameObject fiddle)
    {
        //Debug.Log(go.transform.position + " " + position);
        //Debug.Log(fiddle.transform.position);
        var x = go.GetComponent<RectTransform>();
        Debug.Log(x.position.z);
        LeamnTween.moveLocal(go, new Vector3(position.x, position.y, 0), time);
        EventBus.OnPlayerBatttleDeckAmountChanged?.Invoke();
        Debug.Log(x.position.z);
        yield return new WaitForSecondsRealtime(time);
        Debug.Log(x.position.z);
        //go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0);
        go.transform.SetParent(transform);
        Debug.Log(x.position.z);
        //go.GetComponent<RectTransform>().position = new Vector3(go.GetComponent<RectTransform>().position.x, go.GetComponent<RectTransform>().position.y, 0);
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
