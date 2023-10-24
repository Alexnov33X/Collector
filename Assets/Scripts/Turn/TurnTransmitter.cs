using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ��������� ���� ����������� �����. �������� ������ ��� ��������� ������� ��� ����
/// ��������� ���� ����:
/// - ���� ������ ����
/// - ���� ����� ����
/// </summary>
public class TurnTransmitter : MonoBehaviour
{
    public PlayerHand playerHand;
    public PlayerHand enemyHand;

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
    //public void ExucuteOneTurn()
    //{
    //    StartingPhase();
    //    playerHand.ExecuteHandPhases();
    //    //���� �����(��� ��� ������ �������)
    //    EndingPhase();
    //}
    public void ExucuteOneTurn()
    {
        StartCoroutine(Tasks());
    }
    private IEnumerator Tasks()
    {
        StartingPhase();
        playerHand.ExecuteHandPhases();
        yield return new WaitForSeconds(2.0f); //��������� �������� � 2 ������� ����� ����� ������ � ���������
        enemyHand.ExecuteHandPhases();
        EndingPhase();
    }

    private void StartingPhase()
    {

    }

    private void EndingPhase()
    {

    }

}
