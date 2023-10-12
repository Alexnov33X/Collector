using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        //camAnim = Camera.main.GetComponent<Animator>();
        // availableBattleSlots = new bool[battleSlots.Length];
        // //availableCardSlots = new bool[cardSlots.Length];
        // Debug.Log(Random.Range(0, battleSlots.Length));
        // Debug.Log(this);
        GameObject newCardObject = Instantiate(PlayableCardPrefab, gameObject.transform);
        // Получаем компонент PlayableCard на новом объекте
        PlayableCard newCard = newCardObject.GetComponent<PlayableCard>();
        PlayableCard newCard1 = newCardObject.GetComponent<PlayableCard>();
        newCard.card = card;
        newCard1.card = card;
        //newCardObject.transform.SetParent(transform);
        // и так далее
    }

    // public void DrawCard()
    // {
    // 	if (deck.Count >= 1)
    // 	{
    // 		camAnim.SetTrigger("shake");

    // 		GameCard randomCard = deck[Random.Range(0, deck.Count)];
    // 		for (int i = 0; i < availableCardSlots.Length; i++)
    // 		{
    // 			if (availableCardSlots[i] == true)
    // 			{
    // 				randomCard.gameObject.SetActive(true);
    // 				randomCard.handIndex = i;
    // 				randomCard.transform.position = cardSlots[i].position;
    // 				randomCard.transform.rotation = cardSlots[i].rotation;
    // 				randomCard.hasBeenPlayed = false;
    // 				deck.Remove(randomCard);
    // 				availableCardSlots[i] = false;
    // 				return;
    // 			}
    // 		}
    // 	}
    // }
    public void PlayCard(PlayableCard gc, bool isPlayer)
    {

    }
    // public void PlayCard(GameCard gc)
    // {
    // 	int index = Random.Range(0, battleSlots.Length);
    // 	//while (!availableBattleSlots[index])
    // 	//	index = Random.Range(0, battleSlots.Length);
    // 	//availableBattleSlots[index] = false;
    // 	gc.transform.localPosition = battleSlots[index].position;
    // }

    // public void Shuffle()
    // {
    // 	if (discardPile.Count >= 1)
    // 	{
    // 		foreach (GameCard card in discardPile)
    // 		{
    // 			deck.Add(card);
    // 		}
    // 		discardPile.Clear();
    // 	}
    // }

    // private void Update()
    // {
    // 	deckSizeText.text = deck.Count.ToString();
    // 	discardPileSizeText.text = discardPile.Count.ToString();
    // }
}
