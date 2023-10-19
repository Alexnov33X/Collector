using UnityEngine;

/// <summary>
/// ��������� ���� ����������� �����. �������� ������ ��� ��������� ������� ��� ����
/// ��������� ���� ����:
/// - ���� ������ ����
/// - ���� ����� ����
/// </summary>
public class TurnTransmitter : MonoBehaviour
{
    bool firstPlayerTurn = true;
    int turnCount = 0;

    public PlayerHand playerHand;

    /// <summary>
    /// ��������� ��� ���� ������ ����. 
    /// ������� ���:
    /// - ���� ������ ����
    /// - ���� ����:
    ///     -- ���� �������� ���������
    ///     -- ���� ������ �����
    ///     -- ���� �������
    /// - ���� �����
    /// - ���� ����� ����
    /// </summary>
    public void ExucuteOneTurn()
    {
        StartingPhase();
        playerHand.ExecuteHandPhases();
        //���� �����(��� ��� ������ �������)
        EndingPhase();
    }

    private void StartingPhase()
    {

    }

    private void EndingPhase()
    {

    }

    /// <summary>
    /// Old Methods
    /// </summary>
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
