using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHero : MonoBehaviour
{
    public PlayerHeroDisplay pd;

    private int health = 90;
    public int Health { get => health; set => health = value; }

    void Start()
    {
        pd.updateInformation(health.ToString());
    }

    public void OnHit(int damage)
    {
        Health -= damage;
        pd.updateInformation(Health.ToString());
    }

}
