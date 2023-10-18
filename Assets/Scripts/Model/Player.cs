using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    public PlayerDisplay pd;
    // Start is called before the first frame update
    void Start()
    {
        health = 30;
        attack = 0;
        pd.updateInformation();
    }

   public override void OnHit (GameBoardDisplay gb, int position, Creature Attacker, int damage)
    {
        health -= damage;
        pd.updateInformation();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
