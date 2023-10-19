using UnityEngine;

/// <summary>
/// ¬ыполн€ет роль передатчика ходов. ¬с€ логика св€занна€ с передачей хода должна быть здесь.
/// Ќужно лучше уточнить услови€ игры: какие условие передачи хода, что происходит во врем€ передачи хода, 
/// после передачи, что не может оппонет когда ход не ну него.
/// </summary>
public class TurnTransmitter : MonoBehaviour
{
    bool firstPlayerTurn = true;
    int turnCount = 0;

    public PlayerHand playerHand;
    public PlayerHand enemyHand;

    void TimePassA()
    {
        if (firstPlayerTurn)
        {
            turnCount++;
            TimePassB();
        }
        else
            TimePassB();

        return;
    }

    public void GameTurn()
    {
        Debug.Log("CYCLE " + turnCount);
        for (int i = 0; i < 2; i++)
        {
            TimePassA();
            /*DrawCard(firstPlayerTurn);
            Combat(firstPlayerTurn);*/
            firstPlayerTurn = !firstPlayerTurn;
        }
    }

    public void TimePassB()
    {
        for (int i = 0; i < playerHand.GetHandLength(); i++)
        {
            //playerHand.cardScriptables[i].ChangeTimeCost(-1);
            /*if (handList[i].cardData.TimeCost == 0)
            {
                handList.Remove(handList[i]);
                GameMaster.instance.PlayCard(handList[i]);
                i--;
            }*/
        }
    }
}
