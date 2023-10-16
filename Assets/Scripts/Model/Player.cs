using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public PlayerDisplay pd;
    // Start is called before the first frame update
    void Start()
    {
        health = 30;
        attack = 0;
        pd.updateInformation();
    }

   public override void OnHit (GameBoardView gb, int position, Entity Attacker, int damage)
    {
        health -= damage;
        pd.updateInformation();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
