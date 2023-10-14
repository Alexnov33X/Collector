using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class GameMaster : MonoBehaviour
{
    public List<PlayableCard> playerDeck;
    public List<PlayableCard> enemyDeck;
    public List<PlayableCard> playerHand;
    public List<PlayableCard> enemyHand;
    // public PlayerHand playerHand;
    // public PlayerHand enemyHand;
    //public TextMeshProUGUI deckSizeText;
    public GameBoardView gb;

    public List<PlayableCard> discardPile;
    //public TextMeshProUGUI discardPileSizeText;
    private Animator camAnim;

    bool firstPlayerTurn = true;
    int turnCount = 0;

    void TimePass()
    {
        if (firstPlayerTurn)
        {
            turnCount++;
            foreach (PlayableCard card in playerHand)
            {
                card.card.timeCost--;
                if (card.card.timeCost == 0)
                    PlayCard(card, true);
            }
        }
        else
        {
            foreach (PlayableCard card in enemyHand)
            {
                card.card.timeCost--;
                if (card.card.timeCost == 0)
                    PlayCard(card, false);
            }
        }
    }

    void DrawCard()
    {
        if (firstPlayerTurn && playerDeck.Count > 0 && playerHand.Count < 6)
        {
            PlayableCard randomCard = playerDeck[Random.Range(0, playerDeck.Count)];
            playerDeck.Add(randomCard);
        }
        else if (enemyDeck.Count > 0 && enemyHand.Count < 6)
        {
            PlayableCard randomCard = enemyDeck[Random.Range(0, enemyDeck.Count)];
            enemyDeck.Add(randomCard);
        }
    }

    public GameObject PlayableCardPrefab;
    public CardInfo card;
    private void Start()
    {
        foreach (PlayableCard pc in playerHand)
        if (pc!=null)
        pc.GetComponent<CardDisplay>().HideInformation();
    }

    
    public void PlayCard(PlayableCard gc, bool isPlayer)
    {

    }
}
