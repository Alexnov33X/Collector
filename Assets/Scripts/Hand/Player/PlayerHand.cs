using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

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
    /// ������, �������������� ����� ����, ������ �����(�� ��������� CardEntity)
    /// </summary>
    public List<CardEntity> handList;

    /// <summary>
    /// ���� ������� ����� ���������� � ���� �����, ������� ����� ����� ������ �� ����
    /// </summary>
    private List<CardEntity> removeCardsList;
    private float delay = 0.25f;

    public bool isPlayer; //if true - ������ ��� ���� ������, ����� ��� ���� ���������

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
    /// ��������� ���� ���� �� ������� ������������ ����:
    /// - ���� �������� ���������
    /// - ���� ������ �����
    /// - ���� �������
    /// </summary>
    /// 
    public void ExecuteHandPhases()
    {
        StartCoroutine(Phases());
    }
    public IEnumerator Phases()
    {
        yield return StartCoroutine(CostReductionPhase());
        yield return new WaitForSeconds(1);
        CardTransferPhase();
        yield return new WaitForSeconds(1);
        SummonPhase();
        yield return new WaitForSeconds(1);
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

        GameObject newCardExample = Instantiate(CardPrefab, gameObject.transform);

        CardEntity newCardEntity = newCardExample.GetComponent<CardEntity>();
        newCardEntity.InitializeCard(transferedCard);

        handList.Add(newCardEntity);

        EventBus.OnPlayerBatttleDeckAmountChanged?.Invoke();
    }

    /// <summary>
    /// - ���� �������
    /// </summary>
    private void SummonPhase()
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
