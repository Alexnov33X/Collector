using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Осуществляет логику управления рукой игрока: принятие карты из деки, уменьшение TimeCost карты и выход карты на поле.
/// Нужно доделать и допеределать
/// </summary>
public class PlayerHandController : MonoBehaviour
{
    public List<PlayableCard> handOfCards;
    public GameObject cardPrefab;
    public bool isPlayer;
    GameMaster gm;

    public void TimePass()
    {
        for (int i = 0; i < handOfCards.Count; i++)
        {
            handOfCards[i].ChangeTimeCost(-1);
            if (handOfCards[i].timeCost == 0)
            {
                handOfCards.Remove(handOfCards[i]);
                gm.PlayCard(handOfCards[i], isPlayer);
                i--;
            }
        }
        return;
    }

    public void AddCardToHand(CardInfo ci)
    {
        if (AmountOfCards() < 6)
        {
            GameObject newCard = Instantiate(cardPrefab);
            PlayableCard newPC = newCard.GetComponent<PlayableCard>();
            newPC.card = ci;
            newPC.SetInformationFromSO();
            newCard.GetComponent<CardDisplay>().card = ci;
            newCard.GetComponent<CardDisplay>().HideInformation();
            handOfCards.Add(newPC);
            newCard.transform.SetParent(transform);
            newCard.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Start()
    {
        PlayableCard[] childCards = GetComponentsInChildren<PlayableCard>();
        handOfCards.AddRange(childCards);
        gm = GameMaster.instance;
        //TimePass();
        foreach (PlayableCard pc in handOfCards)
            if (pc != null)
            {
                pc.Start();
                pc.GetComponent<CardDisplay>().HideInformation();
            }
        //TimePass();
    }
    public int AmountOfCards()
    {
        return handOfCards.Count;
    }
}
