using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    // Start is called before the first frame update
     public List<PlayableCard> handOfCards;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimePass(){
        foreach (PlayableCard card in handOfCards)
        {
            card.card.timeCost--;
            // if (card.card.timeCost == 0)
            // dsad
        }
        return;
    }
}
