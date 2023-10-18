using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Осуществляет логику управления рукой игрока: принятие карты из деки, уменьшение TimeCost карты и выход карты на поле.
/// Нужно доделать и допеределать
/// </summary>
public class PlayerHandController : MonoBehaviour
{
    public List<Creature> handOfCards;
    public GameObject cardPrefab;
    public bool isPlayer;

    private void Start()
    {
        /*Creature[] childCards = GetComponentsInChildren<Creature>();
        handOfCards.AddRange(childCards);
        foreach (Creature pc in handOfCards)
        {
            if (pc != null)
            {
                pc.InitializeCard();
                pc.GetComponent<CardDisplay>().HideInformation();
            }
        }*/
    }

    public void TimePass()
    {
        /*for (int i = 0; i < handOfCards.Count; i++)
        {
            handOfCards[i].ChangeTimeCost(-1);
            if (handOfCards[i].card.TimeCost == 0)
            {
                handOfCards.Remove(handOfCards[i]);
                GameMaster.instance.PlayCard(handOfCards[i], isPlayer);
                i--;
            }
        }*/
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

    public int AmountOfCards()
    {
        return handOfCards.Count;
    }
}
