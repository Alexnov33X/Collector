using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public List<CardInfo> playerDeck;
    public List<CardInfo> enemyDeck;
    public PlayerHand playerHand;
    public PlayerHand enemyHand;
    public GameBoardView gb;
    public List<PlayableCard> discardPile;
    private Animator camAnim;

    bool firstPlayerTurn = true;
    int turnCount = 0;

    public GameObject PlayableCardPrefab;
    public CardInfo card;
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

    void DrawCard(bool forPlayer)
    {
        if (forPlayer && playerDeck.Count > 0)
        {
            CardInfo randomCard = playerDeck[Random.Range(0, playerDeck.Count)];
            playerHand.AddCardToHand(randomCard);
            playerDeck.Remove(randomCard);
        }
        else if (!forPlayer && enemyDeck.Count > 0)
        {
            CardInfo randomCard = enemyDeck[Random.Range(0, enemyDeck.Count)];
            enemyHand.AddCardToHand(randomCard);
            enemyDeck.Remove(randomCard);
        }
    }


    private void Start()
    {
        // TimePass();
        // DrawCard(true);
        // DrawCard(true);
    }

    public void GameTurn()
    {
        Debug.Log("CYCLE " + turnCount);
        for (int i = 0; i < 2; i++)
        {
            TimePass();
            DrawCard(firstPlayerTurn);
            Combat(firstPlayerTurn);
            firstPlayerTurn = !firstPlayerTurn;
        }
    }
    public void Combat(bool forPlayer)
    {
        gb.OrderAttack(forPlayer);
    }

    public void PlayCard(PlayableCard gc, bool isPlayer)
    {
        Entity e = gc.SpawnEntity();
        gb.AddEntityToPlayerSide(e, isPlayer);
        Destroy(gc.gameObject);
        Debug.Log("DEAD?");
        //summon creature on player side
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

    void GameOver()
    {

    }
}
