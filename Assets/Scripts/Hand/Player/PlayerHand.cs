using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public GameObject CardPrefab;

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
    /// ��������� ���� ���� �� ������� ������������ ����:
    /// - ���� �������� ���������
    /// - ���� ������ �����
    /// - ���� �������
    /// </summary>
    public void ExecuteHandPhases()
    {
        CostReductionPhase();
        CardTransferPhase();
        SummonPhase();
    }

    /// <summary>
    /// - ���� �������� ���������
    /// </summary>
    private void CostReductionPhase()
    {

    }

    /// <summary>
    /// - ���� ������ �����
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
        }
    }

    /// <summary>
    /// - ���� �������
    /// </summary>
    private void SummonPhase()
    {

    }

    #region Additional Methods for Phases

    /// <summary>
    /// ����������� ��������� ����� �� ������ ������ � ������� �� �� ����.
    /// </summary>
    /// <returns>���������� ScriptableObject �����</returns>     
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
