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

    public PlayerHandController playerHand;
    public PlayerHandController enemyHand;

    void TimePass()
    {
        if (firstPlayerTurn)
        {
            turnCount++;
            playerHand.TimePass();
        }
        else
            enemyHand.TimePass();

        return;
    }

    public void GameTurn()
    {
        Debug.Log("CYCLE " + turnCount);
        for (int i = 0; i < 2; i++)
        {
            TimePass();
            /*DrawCard(firstPlayerTurn);
            Combat(firstPlayerTurn);*/
            firstPlayerTurn = !firstPlayerTurn;
        }
    }
}
