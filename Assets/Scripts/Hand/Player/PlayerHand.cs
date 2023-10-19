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
    /// ����������� ����
    /// </summary>
    private const int HandCapacity = 6;

    #endregion

    public GameObject CardPrefab;

    private List<CardEntity> handList;

    private void Start()
    {
        handList = new List<CardEntity>(HandCapacity);
    }

    public void AddCardToHand()
    {
        if (handList.Count() < 6)
        {
            CardScriptableObject transferedCard = PlayerBattleDeck.TransferCardToHand();
            
            GameObject newCardExample = Instantiate(CardPrefab, gameObject.transform);
            
            CardEntity newCardEntity = newCardExample.GetComponent<CardEntity>();
            newCardEntity.InitializeCard(transferedCard);
            
            handList.Add(newCardEntity);
        }
    }

    public void RemoveCardFromHand()
    {

    }

    public int GetHandLength()
    {
        return handList.Count;
    }

    public CardData GetCardDataFromIndex(int i)
    {
        return handList[i].cardData;
    }
}
