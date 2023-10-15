using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public List<PlayableCard> handOfCards;
    public GameObject cardPrefab;
    public bool isPlayer;
    GameMaster gm;

    public void TimePass()
    {
        for (int i = 0; i < handOfCards.Count; i++)
        {
            PlayableCard pc = handOfCards[i];
            pc.ChangeTimeCost(-1);
            if (pc.timeCost == 0)
            {
                handOfCards.Remove(pc);
                gm.PlayCard(pc, isPlayer);
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
