using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplay : MonoBehaviour
{
      GameMaster gm;
      public TextMeshPro amountOfCards;
      public Text cards;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameMaster.instance;
    }

    // Update is called once per frame
    void Update()
    {
        cards.text= gm.playerDeck.Count.ToString();
    }
}
