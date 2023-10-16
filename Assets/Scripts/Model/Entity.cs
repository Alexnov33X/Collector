using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{


    public Sprite artwork;

    public int timeCost = 1;
    public int attack;
    public int health;
    //Типы и аффинити вероятно нужны существу в бою, но пока можно их делать. Наверное
    // public CardType cardType;
    // public CardAffinity cardAffinity;
    // public CardRarity cardRarity;
    int id;

    public virtual void BeforeAttack(GameBoardView gb, int position)
    {

    }

    public virtual void Attack(GameBoardView gb, int position)
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
    public virtual void attackPlayer(GameBoardView gb, int position)
    { //Надо подумать где должен находится слот игрока и через что к нему обращаться
        if (position < 6)
            gb.enemyPlayer.OnHit(gb, position, this, attack);
        else
            gb.mainPlayer.OnHit(gb, position, this, attack);
    }

    public virtual void AfterAttack(GameBoardView gb, int position)
    {

    }

    public virtual void BeforeHit(GameBoardView gb, int position, Entity Attacker, int damage)
    {

    }

    public virtual void OnHit(GameBoardView gb, int position, Entity Attacker, int damage)
    {
        health -= damage;
    }

    public virtual void AfterHit(GameBoardView gb, int position, Entity Attacker, int damage)
    {

    }

    public virtual void OnPlay(GameBoardView gb, int position)
    {

    }

    public virtual void OnDeath(GameBoardView gb, int position)
    {

    }
}
