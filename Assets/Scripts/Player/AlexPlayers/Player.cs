using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CardEntity
{
    public PlayerDisplay pd;
    // Start is called before the first frame update
    void Start()
    {
        cardData.Health = 30;
        cardData.Attack = 0;
        pd.updateInformation();
    }

   public override void OnHit (GameBoardDisplay gb, int position, CardEntity Attacker, int damage)
    {
        cardData.Health -= damage;
        pd.updateInformation();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
