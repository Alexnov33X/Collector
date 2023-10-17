using UnityEngine;
using static Enums;

public class PlayableCard : MonoBehaviour
{
    public CardInfo card;

    [HideInInspector] public int timeCost;
    [HideInInspector] public int attack;
    [HideInInspector] public int health;
    [HideInInspector] public CardType cardType;
    [HideInInspector] public CardUniverse cardAffinity;
    
    private CardDisplay cd;

    public void Start()
    {
        SetInformationFromSO();
        cd = GetComponent<CardDisplay>();
    }

    public void ChangeTimeCost(int change)
    {
        timeCost += change;
        cd?.UpdateInformation();
    }

    public void SetInformationFromSO()
    {
        timeCost = card.TimeCost;
        attack = card.Attack;
        health = card.Health;
        cardType = card.CardType;
        cardAffinity = card.CardAffinity;
    }
}
