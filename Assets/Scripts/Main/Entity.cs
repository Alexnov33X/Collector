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

    public void BeforeAttack(GameBoard gb, int position)
    {

    }

    public void Attack(GameBoard gb, int position)
    {

    }

    public void AfterAttack(GameBoard gb, int position)
    {

    }

    public void BeforeHit(GameBoard gb, int position, Entity Attacker, int damage)
    {

    }

    public void OnHit(GameBoard gb, int position, Entity Attacker, int damage)
    {
        health -= damage;
    }

    public void AfterHit(GameBoard gb, int position, Entity Attacker, int damage)
    {

    }

    public void OnPlay(GameBoard gb, int position)
    {

    }

    public void OnDeath(GameBoard gb, int position)
    {

    }
}
