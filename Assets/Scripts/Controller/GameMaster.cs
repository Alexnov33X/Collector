using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public List<PlayableCard> playerDeck;
    public List<PlayableCard> enemyDeck;
    public PlayerHand playerHand;
    public PlayerHand enemyHand;
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
            playerHand.TimePass();
        }
        else
            enemyHand.TimePass();
        
        return;
    }

    void DrawCard()
    {
        if (firstPlayerTurn && playerDeck.Count > 0 && playerHand.AmountOfCards() < 6)
        {
            PlayableCard randomCard = playerDeck[Random.Range(0, playerDeck.Count)];
            playerHand.AddCardToHand(randomCard);
            playerDeck.Remove(randomCard);
        }
        else if (enemyDeck.Count > 0 && enemyHand.AmountOfCards() < 6)
        {
            PlayableCard randomCard = enemyDeck[Random.Range(0, enemyDeck.Count)];
            enemyDeck.Add(randomCard);
        }
    }

    public GameObject PlayableCardPrefab;
    public CardInfo card;
    private void Start()
    {
TimePass();
//DrawCard();
    }


    public void PlayCard(PlayableCard gc, bool isPlayer)
    {

    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Устанавливаем GameMaster как единственный экземпляр
        }
        else
        {
            Destroy(gameObject); // Уничтожаем новые объекты GameMaster, чтобы сохранить только один
        }
    }
}
