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


    void Start()
    {
        EventBus.OnGameVictory += Victory; //������������� �� ������� ��������� ����
        EventBus.OnGameLoss += Loss;
        StartCoroutine(Tasks()); //��������� ������� ����
    }
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
    /// �� ������ ������ ����� �������� �������� +
    /// ������ ��� �������� ������������� ��� ������, 
    /// ������ � ������������� ���������.
    /// ��� �������� � ���� ��� �������� ������ 
    /// ����������� �� ��������� ���
    /// </summary>
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
        yield return new WaitForSeconds(2);
        EndingPhase();
    }

    private void StartingPhase()
    {

    }

    private void EndingPhase()
    {
        StartCoroutine(Tasks()); //���� ��� ��� ������ ������������� ������� ����
    }

    /// <summary>
    /// ������ ������ � ���������. � ��� ��
    /// ������������� ��� ��������, � ��������� ��� ���������� �����������
    /// � ���� �� ������, �� ����� ����������� ����� �������� ������� �� �����������
    /// </summary>
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
