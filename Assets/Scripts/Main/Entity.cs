using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //  public new string name;
    //     public string description;

    public Sprite artwork;

    public int timeCost = 1;
    public int attack;
    public int health;

    // public CardType cardType;
    // public CardAffinity cardAffinity;
    // public CardRarity cardRarity;
    int id;

    public void BeforeAttack(GameBoardView gb, int position)
    {

    }

    public void Attack(GameBoardView gb, int position)
    {

    }

    public void AfterAttack(GameBoardView gb, int position)
    {

    }

    public void BeforeHit(GameBoardView gb, int position, Entity Attacker, int damage)
    {

    }

    public void OnHit(GameBoardView gb, int position, Entity Attacker, int damage)
    {
        health -= damage;
    }

    public void AfterHit(GameBoardView gb, int position, Entity Attacker, int damage)
    {

    }

    public void OnPlay(GameBoardView gb, int position)
    {

    }

    public void OnDeath(GameBoardView gb, int position)
    {

    }
}
