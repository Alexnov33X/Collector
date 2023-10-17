using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public GameBoardView gb;
    public List<PlayableCard> discardPile;

    public GameObject PlayableCardPrefab;
    public CardInfo card;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void Combat(bool forPlayer)
    {
        gb.OrderAttack(forPlayer);
    }

    public void PlayCard(PlayableCard gc, bool isPlayer)
    {
        Destroy(gc.gameObject);
        Debug.Log("DEAD?");
        //summon creature on player side
    }

}
