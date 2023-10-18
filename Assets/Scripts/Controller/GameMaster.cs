using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// ��� God-object. � ���� ��� ����� ���������� ������� ���������������. ����� �� ����� ������� � ���������������.
/// �������� ��������������
/// </summary>
public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public GameBoardDisplay gb;
    public List<CardBoardBehaviour> discardPile;

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

    public void PlayCard(CardBoardBehaviour gc, bool isPlayer)
    {
        Destroy(gc.gameObject);
        Debug.Log("DEAD?");
        //summon creature on player side
    }

}
