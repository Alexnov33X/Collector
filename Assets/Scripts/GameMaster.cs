using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// это God-object. У него нет одной конкретной области ответственности. Такое мы будем убивать и декомпозировать.
/// Частично декомпозировал
/// </summary>
public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public GameBoardDisplay gb;

    public GameObject PlayableCardPrefab;
    public CardScriptableObject card;

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

    public void PlayCard(CardEntity cardEntity)
    {
        Destroy(cardEntity.gameObject);
        Debug.Log("DEAD?");
        //summon creature on player side
    }

}
