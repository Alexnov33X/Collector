using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    public VictoryScreen vs;
    public Button turnStep;

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
    void Start()
    {
        EventBus.OnGameVictory += Victory;
        EventBus.OnGameLoss += Loss;
        StartCoroutine(Tasks());
    }
    public void ExucuteOneTurn()
    {
        StartCoroutine(Tasks());
    }
    private IEnumerator Tasks()
    {
        yield return new WaitForSeconds(1);
        StartingPhase();
        playerHand.ExecuteHandPhases();
        yield return new WaitForSeconds(2);
        enemyHand.ExecuteHandPhases();
        yield return new WaitForSeconds(1);
        EndingPhase();
    }

    private void StartingPhase()
    {

    }

    private void EndingPhase()
    {
        StartCoroutine(Tasks());
    }

    private void Victory()
    {
        StopAllCoroutines();
        vs.EndGame(true);
        turnStep.gameObject.SetActive(false);

    }

    private void Loss()
    {
        StopAllCoroutines();
        vs.EndGame(false);
        turnStep.gameObject.SetActive(false);
    }

}
