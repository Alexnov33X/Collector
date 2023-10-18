using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public Sprite artwork;

    public int timeCost = 1;
    public int attack;
    public int health;

    public CardScriptableObject card;

    [HideInInspector] public CardData cardData;

    private void Start()
    {
        InitializeCard();
    }

    public void InitializeCard()
    {
        cardData = new CardData(card);
    }

    public virtual void BeforeAttack(GameBoardDisplay gb, int position)
    {

    }

    public virtual void Attack(GameBoardDisplay gb, int position)
    {
        if (position < 6) //Player creature
        {
            if (gb.occupants[6 + position % 3] != null)
                gb.occupants[6 + position % 3].OnHit(gb, position, this, attack);
            else if (gb.occupants[6 + 3 + position % 3] != null)
                gb.occupants[6 + 3 + position % 3].OnHit(gb, position, this, attack);
            else
            attackPlayer(gb, position);
        }
        else //Enemy Creature
        {
            if (gb.occupants[position % 3] != null)
                gb.occupants[position % 3].OnHit(gb, position, this, attack);
            else if (gb.occupants[3 + position % 3] != null)
                gb.occupants[3 + position % 3].OnHit(gb, position, this, attack);
            else
            attackPlayer(gb, position); 
        }
    }
    public virtual void attackPlayer(GameBoardDisplay gb, int position)
    { //Надо подумать где должен находится слот игрока и через что к нему обращаться
        if (position < 6)
            gb.enemyPlayer.OnHit(gb, position, this, attack);
        else
            gb.mainPlayer.OnHit(gb, position, this, attack);
    }

    public virtual void AfterAttack(GameBoardDisplay gb, int position)
    {

    }

    public virtual void BeforeHit(GameBoardDisplay gb, int position, Creature Attacker, int damage)
    {

    }

    public virtual void OnHit(GameBoardDisplay gb, int position, Creature Attacker, int damage)
    {
        health -= damage;
    }

    public virtual void AfterHit(GameBoardDisplay gb, int position, Creature Attacker, int damage)
    {

    }

    public virtual void OnPlay(GameBoardDisplay gb, int position)
    {

    }

    public virtual void OnDeath(GameBoardDisplay gb, int position)
    {

    }
}
