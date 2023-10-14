using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;
public class PlayableCard : MonoBehaviour
{
    public CardInfo card;
    public int timeCost;
    public int attack;
    public int health;
    public CardType cardType;
    public CardAffinity cardAffinity;
    CardDisplay cd;
    public void ChangeTimeCost(int change)
    {
        timeCost += change;
        if (cd.gameObject != null)
            cd.updateInformation();
    }
    public void SetInformationFromSO()
    {
        timeCost = card.timeCost;
        attack = card.attack;
        health = card.health;
        cardType = card.cardType;
        cardAffinity = card.cardAffinity;
    }
    public void OnPlay()
    {
        //SetInformationFromSO();
    }

    public void Start()
    {
        SetInformationFromSO();
        cd = GetComponent<CardDisplay>();
    }
    public GameObject SpawnThing()
    {
        return Instantiate(this.gameObject);
    }

}
