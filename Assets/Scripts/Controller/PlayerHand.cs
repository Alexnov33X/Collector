using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public List<PlayableCard> handOfCards;

    public void TimePass()
    {
        foreach (PlayableCard card in handOfCards)
        {
            card.card.timeCost--;
            // if (card.card.timeCost == 0)
        }
        return;
    }
}
