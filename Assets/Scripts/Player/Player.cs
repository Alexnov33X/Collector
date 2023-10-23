using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : CardEntity
{
    //НАДО СДЕЛАТЬ Дисплея отображения игрока, можно такой же как у CardOnBoard
    public PlayerDisplay pd;
    // Start is called before the first frame update
    void Start()
    {
        cardData.Health = 30;
        cardData.Attack = 0;
        pd.updateInformation();
    }

    public override void OnHit(GameBoardDisplay gb, int position, CardEntity Attacker, int damage)
    {
        cardData.Health -= damage;
        pd.updateInformation();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
