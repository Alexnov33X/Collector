using UnityEngine;

/// <summary>
/// ��������� ���� ����������� �����. �������� ������ ��� ��������� ������� ��� ����
/// ��������� ���� ����:
/// - ���� ������ ����
/// - ���� ����� ����
/// </summary>
public class TurnTransmitter : MonoBehaviour
{
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
}
