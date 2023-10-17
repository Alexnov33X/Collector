using UnityEngine;

/// <summary>
/// ��������� ���� ����������� �����. ��� ������ ��������� � ��������� ���� ������ ���� �����.
/// ����� ����� �������� ������� ����: ����� ������� �������� ����, ��� ���������� �� ����� �������� ����, 
/// ����� ��������, ��� �� ����� ������� ����� ��� �� �� ����.
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
