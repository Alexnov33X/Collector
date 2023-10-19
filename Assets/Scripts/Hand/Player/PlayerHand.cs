using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Сущность. Хранит в себе данные о картах в руке игрока. Может обрабатывать эту инфу.
/// </summary>
public class PlayerHand : MonoBehaviour
{
    #region Constants

    /// <summary>
    /// Вместимость руки
    /// </summary>
    private const int HandCapacity = 6;

    #endregion

    /// <summary>
    /// 
    /// </summary> 
    private List<CardEntity> handList;

    private void Start()
    {
        handList = new List<CardEntity>(HandCapacity);
    }

    public void TimePass()
    {
        for (int i = 0; i < handList.Count; i++)
        {
            //playerHand.cardScriptables[i].ChangeTimeCost(-1);
            if (handList[i].cardData.TimeCost == 0)
            {
                handList.Remove(handList[i]);
                GameMaster.instance.PlayCard(handList[i]);
                i--;
            }
        }
    }

    public void AddCardToHand(CardScriptableObject cardSO)
    {
        /*if (AmountOfCards() < 6)
        {
            GameObject newCard = Instantiate(cardPrefab);
            
            CardBoardBehaviour cardBehaviour = newCard.GetComponent<CardBoardBehaviour>();
            cardBehaviour.card = cardSO;
            cardBehaviour.InitializeCard();

            //newCard.GetComponent<CardDisplay>().cardBehaviour = cardSO;
            newCard.GetComponent<CardDisplay>().HideInformation();
            handOfCards.Add(cardBehaviour);
            newCard.transform.SetParent(transform);
            newCard.transform.localScale = new Vector3(1, 1, 1);
        }*/
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

    public void InitializeCardDataFromIndex(int i)
    {
        handList[i].InitializeCard();
    }
}
