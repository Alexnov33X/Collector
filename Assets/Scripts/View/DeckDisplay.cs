using TMPro;
using UnityEngine;

public class DeckDisplay : MonoBehaviour
{
      public TextMeshProUGUI amountOfCards;

    void Update()
    {
        amountOfCards.text= GameMaster.instance.playerDeck.Count.ToString();
    }
}
