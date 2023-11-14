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

    private float gameStartDelay = 1f;
    private float delayBetweenPlayerAndEnemy = 2f;

    void Start()
    {
        EventBus.OnGameVictory += Victory; //������������� �� ������� ��������� ����
        EventBus.OnGameLoss += Loss;
        gameStartDelay = AnimationAndDelays.instance.gameStartDelay;
        delayBetweenPlayerAndEnemy = AnimationAndDelays.instance.delayBetweenPlayerAndEnemy;
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
        yield return new WaitForSeconds(gameStartDelay);
        StartingPhase();
        yield return playerHand.ExecuteHandPhases();
        yield return new WaitForSeconds(delayBetweenPlayerAndEnemy);
        yield return enemyHand.ExecuteHandPhases();
        yield return new WaitForSeconds(AnimationAndDelays.instance.delayBetweenTurns);
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
        playerHand.StopAllCoroutines();
        enemyHand.StopAllCoroutines();
        vs.EndGame(true);
        turnStep.gameObject.SetActive(false);
    }

    private void Loss()
    {
        StopAllCoroutines();
        playerHand.StopAllCoroutines();
        enemyHand.StopAllCoroutines();
        vs.EndGame(false);
    }
    

    public void DrawCardsForPlayer(int amount, bool isPlayer)
    {
        if (isPlayer)
            StartCoroutine(playerHand.DrawCardPhase(amount));
        else
            StartCoroutine(enemyHand.DrawCardPhase(amount));
    }

}
