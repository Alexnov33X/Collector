using UnityEngine;
using static Enums;

public class PlayableCard : MonoBehaviour
{
    public CardInfo card;

    [HideInInspector] public int timeCost;
    [HideInInspector] public int attack;
    [HideInInspector] public int health;
    [HideInInspector] public CardType cardType;
    [HideInInspector] public CardAffinity cardAffinity;
    
    private CardDisplay cd;

    public void ChangeTimeCost(int change)
    {
        timeCost += change;
        if (cd.gameObject != null)
            cd.UpdateInformation();
    }
    public void SetInformationFromSO()
    {
        timeCost = card.TimeCost;
        attack = card.Attack;
        health = card.Health;
        cardType = card.CardType;
        cardAffinity = card.CardAffinity;
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
